using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapDefinitionHelper {

    public static IEnumerable<MapDefinition> ConvertToMapDefinitions(Compilation compilation, ImmutableArray<(ClassDeclarationSyntax, InvocationExpressionSyntax[])> profiles, CancellationToken token) {
        var allDefinitions = new List<MapDefinition>();

        for (var i = 0; i < profiles.Length; i++) {
            (var classSyntax, var invocations) = profiles[i];
            var definition = new MapDefinition(classSyntax);

            for (var j = 0; j < invocations.Length; j++) {
                var invocation = invocations[j];
                if (TryExtractCreateMapOrProjectionInvocation(invocation, compilation, token, out var mapOrProjection)) {
                    definition.Mappings.Add(ConvertToMapping(mapOrProjection, token));
                    continue;
                }

                if (TryExtractRecognizedSourcePrefixes(invocation, compilation, token, out var prefixes)) {
                    definition.RecognizedSourcePrefixes.AddRange(prefixes);
                    continue;
                }

                if (TryExtractRecognizedDestinationPrefixes(invocation, compilation, token, out prefixes)) {
                    definition.RecognizedDestinationPrefixes.AddRange(prefixes);
                    continue;
                }
            }

            if (definition.Mappings.Any()) {
                allDefinitions.Add(definition);
            }
        }

        // TODO: Check for duplicate mapping definitions. Add a diagnostic if any are found

        return allDefinitions;
    }

    private static bool TryExtractRecognizedSourcePrefixes(InvocationExpressionSyntax node, Compilation compilation, CancellationToken token, out string[] prefixes) 
        => TryExtractRecognizedPrefixes("RecognizePrefixes", node, compilation, token, out prefixes);

    private static bool TryExtractRecognizedDestinationPrefixes(InvocationExpressionSyntax node, Compilation compilation, CancellationToken token, out string[] prefixes)
        => TryExtractRecognizedPrefixes("RecognizeDestinationPrefixes", node, compilation, token, out prefixes);

    private static bool TryExtractRecognizedPrefixes(string methodName, InvocationExpressionSyntax node, Compilation compilation, CancellationToken token, out string[] prefixes) {
        prefixes = Array.Empty<string>();
        if (!TryExtractMapProfileMethodInvocation(node, compilation, token, methodName, out var _, out var _)) {
            return false;
        }

        var discovered = new List<string>();
        for (var i = 0; i < node.ArgumentList.Arguments.Count; i++) {
            var arg = node.ArgumentList.Arguments[i];
            if (arg.Expression is LiteralExpressionSyntax { RawKind: (int)SyntaxKind.StringLiteralExpression } literal) {
                discovered.Add(literal.Token.ValueText);
            }
        }
        prefixes = discovered.ToArray();
        return true;
    }

    private static bool TryExtractCreateMapOrProjectionInvocation(InvocationExpressionSyntax node, Compilation compilation, CancellationToken token, [NotNullWhen(true)] out MapOrProjectionInvocation? invocation) {
        invocation = null;
        if (!TryExtractGenericName(node.Expression, out var genericName)) {
            return false;
        }

        if (!TryExtractMapProfileMethodInvocation(genericName, compilation, token, new[] { "CreateMap", "CreateProjection" }, out var semanticModel, out var symbol)) {
            return false;
        }

        invocation = new(node, genericName, symbol, semanticModel);
        return true;
    }

    private static bool TryExtractMapProfileMethodInvocation(ExpressionSyntax expressionSyntax, Compilation compilation, CancellationToken token, string desiredMethod, 
        [NotNullWhen(true)] out SemanticModel? semanticModel, [NotNullWhen(true)] out IMethodSymbol? methodSymbol)
        => TryExtractMapProfileMethodInvocation(expressionSyntax, compilation, token, new[] { desiredMethod }, out semanticModel, out methodSymbol);

    private static bool TryExtractMapProfileMethodInvocation(ExpressionSyntax expressionSyntax, Compilation compilation, CancellationToken token, string[] desiredMethods, 
        [NotNullWhen(true)] out SemanticModel? semanticModel, [NotNullWhen(true)] out IMethodSymbol? methodSymbol) {

        methodSymbol = null;
        semanticModel = compilation.GetSemanticModel(expressionSyntax.SyntaxTree);
        // TODO: If I add the reference, how could I make this better?
        if (semanticModel.GetSymbolInfo(expressionSyntax, token) is { Symbol: IMethodSymbol symbol }
            && symbol.ContainingType.ToDisplayString() == "AutomapGenerator.MapProfile"
            && desiredMethods.Contains(symbol.Name)) {

            methodSymbol = symbol;
            return true;
        }
        return false;
    }

    private static MapDefinition.Mapping ConvertToMapping(MapOrProjectionInvocation invocation, CancellationToken token) {
        var projectionOnly = invocation.Symbol.Name == "CreateProjection";
        var sourceSymbol = GetTypeSymbol(invocation.Semantic, invocation.GenericName.TypeArgumentList.Arguments[0], token);
        var destinationSymbol = GetTypeSymbol(invocation.Semantic, invocation.GenericName.TypeArgumentList.Arguments[1], token);

        return new(sourceSymbol.ToDisplayString(),
            GetAllPropertySymbols(sourceSymbol),
            destinationSymbol.ToDisplayString(),
            GetWritablePropertySymbols(destinationSymbol),
            projectionOnly,
            GetCustomMappings(invocation.Origin));
    }

    private static Dictionary<string, MappingCustomization> GetCustomMappings(InvocationExpressionSyntax node) {
        var invocation = node;
        var customMappings = new Dictionary<string, MappingCustomization>();

        while (invocation?.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: "ForMember"} memberExpr) {
            var args = invocation.ArgumentList.Arguments;
            invocation = memberExpr.Expression as InvocationExpressionSyntax;

            // Get the name of the destination property to custom map
            if (args[0].Expression is not SimpleLambdaExpressionSyntax { Body: MemberAccessExpressionSyntax destExpr }) {
                continue;
            }
            var destinationProp = destExpr.Name.Identifier.Text;

            if (ShouldIgnore(args[1].Expression)) {
                customMappings[destinationProp] = new MappingCustomization(ignore: true);
                continue;
            }

            // Check if we have an explicit mapping
            if (TryGetMapFromInvocation(args[1].Expression, out var mapFromInvocation)) {
                if (TryGetExplicitMapping(mapFromInvocation, out var explicitMapping)) {
                    customMappings[destinationProp] = new MappingCustomization(explicitMapping: explicitMapping);
                }
                continue;
            }
        }
        return customMappings;
    }

    private static bool ShouldIgnore(ExpressionSyntax expression) => expression is SimpleLambdaExpressionSyntax {
        Body: InvocationExpressionSyntax {
            Expression: MemberAccessExpressionSyntax { Name.Identifier.Text: "Ignore" }
        }
    };

    private static bool TryGetMapFromInvocation(ExpressionSyntax expression, [NotNullWhen(true)] out InvocationExpressionSyntax? mapFromInvocation) {
        if (expression is SimpleLambdaExpressionSyntax {
            Body: InvocationExpressionSyntax {
                Expression: MemberAccessExpressionSyntax { Name.Identifier.Text: "MapFrom" }
            } invocation
        }) {
            mapFromInvocation = invocation;
            return true;
        }

        mapFromInvocation = null;
        return false;
    }

    private static bool TryGetExplicitMapping(InvocationExpressionSyntax mapFromInvocation, [NotNullWhen(true)] out char[]? mapping) {
        if (mapFromInvocation.ArgumentList.Arguments[0].Expression is not SimpleLambdaExpressionSyntax lambdaExpr) {
        mapping = null;
            return false;
        }

        // TODO: If I want to add support for more robust expressions,
        //       I could use this. However, it would require a lot of other changes
        //       to accomodate expressions beyond simple member access due to the lambda
        //       parameter name.
        //mapping = lambdaExpr.Body.ToFullString();

        var srcMemberChain = new List<string>();
        var srcExpression = lambdaExpr.Body as MemberAccessExpressionSyntax;
        while (srcExpression is not null) {
            srcMemberChain.Insert(0, srcExpression.Name.Identifier.Text);
            srcExpression = srcExpression.Expression as MemberAccessExpressionSyntax;
        }

        if (srcMemberChain.Count == 0) {
            mapping = null;
            return false;
        }
        mapping = string.Join(".", srcMemberChain).ToCharArray();
        return true;
    }

    private static ITypeSymbol GetTypeSymbol(SemanticModel semanticModel, TypeSyntax sourceType, CancellationToken token) 
        => semanticModel.GetTypeInfo(sourceType, token) is not { Type: ITypeSymbol sourceSymbol }
            ? throw new Exception("TODO: How did we get here??")
            : sourceSymbol;

    private static ImmutableArray<IPropertySymbol> GetWritablePropertySymbols(ITypeSymbol destinationSymbol) {
        var destProperties = new List<IPropertySymbol>();
        var destMembers = destinationSymbol.GetMembers();
        for (var i = 0; i < destMembers.Length; i++) {
            var destMember = destMembers[i];
            if (destMember is IPropertySymbol { IsReadOnly: false } destProp) {
                destProperties.Add(destProp);
            }
        }

        return ImmutableArray.CreateRange(destProperties);
    }

    private static ImmutableArray<IPropertySymbol> GetAllPropertySymbols(ITypeSymbol sourceSymbol) {
        var sourceProperties = new List<IPropertySymbol>();
        var sourceMembers = sourceSymbol.GetMembers();
        for (var i = 0; i < sourceMembers.Length; i++) {
            var srcMember = sourceMembers[i];
            if (srcMember is IPropertySymbol srcProp) {
                sourceProperties.Add(srcProp);
            }
        }

        return ImmutableArray.CreateRange(sourceProperties);
    }

    private static bool TryExtractGenericName(ExpressionSyntax expression, [NotNullWhen(true)] out GenericNameSyntax? genericName) {
        while (expression is MemberAccessExpressionSyntax { Expression: InvocationExpressionSyntax invokeExpr }) {
            expression = invokeExpr.Expression;
        }
        if (expression is GenericNameSyntax { TypeArgumentList.Arguments.Count: 2 } matchedName) {
            genericName = matchedName;
            return true;
        }
        genericName = null;
        return false;
    }

    private record MapOrProjectionInvocation(InvocationExpressionSyntax Origin, GenericNameSyntax GenericName, IMethodSymbol Symbol, SemanticModel Semantic);
}
