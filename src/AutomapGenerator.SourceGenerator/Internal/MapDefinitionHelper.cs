using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapDefinitionHelper {

    public static IEnumerable<MapDefinition> ConvertToMapDefinitions(Compilation compilation, ImmutableArray<InvocationExpressionSyntax> invocations, CancellationToken token) {
        var definitions = new List<MapDefinition>();

        for (var i = 0; i < invocations.Length; i++) {
            var invocation = invocations[i];
            if (ConvertToMapDefinition(invocation, compilation, token) is { } def) {
                definitions.Add(def);
            }
        }

        // TODO: Check for duplicate mapping definitions. Add a diagnostic if any are found

        return definitions;
    }

    private static MapDefinition? ConvertToMapDefinition(InvocationExpressionSyntax node, Compilation compilation, CancellationToken token) {
        if (!TryExtractGenericName(node.Expression, out var genericName)) {
            return null;
        }

        var semanticModel = compilation.GetSemanticModel(genericName.SyntaxTree);
        if (semanticModel.GetSymbolInfo(genericName, token) is not { Symbol: IMethodSymbol symbol }) {
            return null;
        }

        // TODO: If I add the reference, how could I make this better?
        if (symbol.ContainingType.ToDisplayString() != "AutomapGenerator.MapProfile") {
            return null;
        }

        var projectionOnly = true;
        if (symbol.Name == "CreateMap") {
            projectionOnly = false;
        } else if (symbol.Name != "CreateProjection") {
            return null;
        }

        var sourceSymbol = GetTypeSymbol(semanticModel, genericName.TypeArgumentList.Arguments[0], token);
        var destinationSymbol = GetTypeSymbol(semanticModel, genericName.TypeArgumentList.Arguments[1], token);

        return new(sourceSymbol.ToDisplayString(),
            GetAllPropertySymbols(sourceSymbol),
            destinationSymbol.ToDisplayString(),
            GetWritablePropertySymbols(destinationSymbol),
            projectionOnly,
            GetCustomMappings(node));
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
}
