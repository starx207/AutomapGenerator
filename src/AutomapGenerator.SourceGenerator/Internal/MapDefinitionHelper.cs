using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapDefinitionHelper {

    // TODO: What about when a profile defines projection only, but we call Map on the types?
    public static IEnumerable<MapDefinition> CombineProfilesAndAdHocMappings(ImmutableArray<MapDefinition> profileMappings, ImmutableArray<MapDefinition> adHocMappings) {
        var allMappings = new List<MapDefinition>(profileMappings);
        for (var i = 0; i < adHocMappings.Length; i++) {
            var adHocMapping = adHocMappings[i];
            var indexesToRemove = new List<int>();
            for (var j = 0; j < adHocMapping.Mappings.Count; j++) {
                var mapping = adHocMapping.Mappings[j];
                if (MappingAlreadyDefined(mapping, allMappings.SelectMany(m => m.Mappings).ToList())) {
                    indexesToRemove.Add(j);
                }
            }
            // Sort descending so we work from back of list (or we'll mess up indexing as we remove items)
            indexesToRemove.Sort((x, y) => y.CompareTo(x));
            for (var j = 0; j < indexesToRemove.Count; j++) {
                var index = indexesToRemove[j];
                if (index < adHocMapping.Mappings.Count) {
                    adHocMapping.RemoveMappingAt(index);
                }
            }
            if (adHocMapping.Mappings.Count > 0) {
                allMappings.Add(adHocMapping);
            }
        }

        return allMappings;
    }

    public static IEnumerable<MapDefinition> PruneAdHocDefinitions(List<MapDefinition> adHocDefs) {
        var pruned = new List<MapDefinition.Mapping>();
        var projections = new List<MapDefinition.Mapping>();

        // Split mappings into projections and maps
        for (var i = 0; i < adHocDefs.Count; i++) {
            for (var j = 0; j < adHocDefs[i].Mappings.Count; j++) {
                var mapping = adHocDefs[i].Mappings[j];
                if (mapping.ProjectionOnly) {
                    projections.Add(mapping);
                } else {
                    if (!MappingAlreadyDefined(mapping, pruned)) {
                        pruned.Add(mapping);
                    }
                }
            }
        }

        // Add projections that are not already mapped
        for (var i = 0; i < projections.Count; i++) {
            var projection = projections[i];
            var found = false;
            for (var j = 0; j < pruned.Count; j++) {
                var other = pruned[j];
                if (projection.SourceName.Equals(other.SourceName) && projection.DestinationName.Equals(other.DestinationName)) {
                    found = true;
                    break;
                }
            }
            if (!found) {
                pruned.Add(projection);
            }
        }

        var newDef = new MapDefinition();
        newDef.AddMappings(pruned);
        return new[] { newDef };
    }

    public static List<MapDefinition> ConvertToMapDefinitions(Compilation compilation, ImmutableArray<(SyntaxNode, InvocationExpressionSyntax[])> profiles, CancellationToken token) {
        var allDefinitions = new List<MapDefinition>();

        for (var i = 0; i < profiles.Length; i++) {
            (var syntaxNode, var invocations) = profiles[i];
            var definition = new MapDefinition();
            var isAdHoc = syntaxNode is InvocationExpressionSyntax;

            for (var j = 0; j < invocations.Length; j++) {
                var invocation = invocations[j];
                if (isAdHoc) {
                    var semantic = compilation.GetSemanticModel(invocation.SyntaxTree);
                    var srcSymbol = GetTypeSymbol(semantic, invocation.ArgumentList.Arguments[0].Expression, token);
                    ITypeSymbol destSymbol;

                    var projection = false;
                    if (TryExtractGenericName(invocation.Expression, 1, out var genericName)) {
                        // Destination type is the 1st type argument
                        destSymbol = GetTypeSymbol(semantic, genericName.TypeArgumentList.Arguments[0], token);
                        if (genericName.Identifier.Text == "ProjectTo") {
                            projection = true;
                        }
                    } else {
                        // Destination type is the 2nd method argument
                        destSymbol = GetTypeSymbol(semantic, invocation.ArgumentList.Arguments[1].Expression, token);
                    }

                    if (projection) {
                        // The source is an IQueryable<T>. We need to get just the T
                        srcSymbol = ((INamedTypeSymbol)srcSymbol).TypeArguments[0];
                    }


                    // Once we have the source type and destination type, we can create a map definition
                    // If the source and destination are both IEnumerable, we need to add the mappings for their generic type too
                    if ((!projection) && TryGetEnumerableTypeSymbol(srcSymbol, out var srcEnumerable) && TryGetEnumerableTypeSymbol(destSymbol, out var destEnumerable)) {
                        // Create the outer mapping
                        var mapType = CollectionMapType.Enumerable;
                        if (destSymbol.TypeKind == TypeKind.Array) {
                            mapType = CollectionMapType.Array;
                        } else if (TryGetSetTypeSymbol(destSymbol, out _)) {
                            mapType = CollectionMapType.Set;
                        }

                        var innerSource = srcEnumerable.TypeArguments[0];
                        var innerDest = destEnumerable.TypeArguments[0];

                        definition.AddMapping(new(
                            srcSymbol,
                            GetAllPropertySymbols(srcSymbol),
                            destSymbol,
                            GetAllPropertySymbols(destSymbol, writableOnly: true),
                            GetAllPublicConstructors(destSymbol),
                            projection,
                            new(),
                            new(mapType, innerSource.ToDisplayString(), innerDest.ToDisplayString())));

                        // Create the inner mapping
                        definition.AddMapping(new(
                            innerSource,
                            GetAllPropertySymbols(innerSource),
                            innerDest,
                            GetAllPropertySymbols(innerDest, writableOnly: true),
                            GetAllPublicConstructors(innerDest),
                            false,
                            new(),
                            MapDefinition.CollectionMapping.NoCollection));
                    } else {
                        definition.AddMapping(new(
                            srcSymbol,
                            GetAllPropertySymbols(srcSymbol),
                            destSymbol,
                            GetAllPropertySymbols(destSymbol, writableOnly: true),
                            GetAllPublicConstructors(destSymbol),
                            projection,
                            new(),
                            MapDefinition.CollectionMapping.NoCollection));
                    }
                } else {
                    if (TryExtractCreateMapOrProjectionInvocation(invocation, compilation, token, out var mapOrProjection)) {
                        definition.AddMapping(ConvertToMapping(mapOrProjection, token));
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
            }

            if (definition.Mappings.Any()) {
                allDefinitions.Add(definition);
            }
        }

        // TODO: Check for duplicate mapping definitions. Add a diagnostic if any are found

        return allDefinitions;
    }

    private static bool MappingAlreadyDefined(MapDefinition.Mapping mapping, List<MapDefinition.Mapping> allMappings) {
        for (var i = 0; i < allMappings.Count; i++) {
            var existingMapping = allMappings[i];
            if (mapping.SourceName.Equals(existingMapping.SourceName) && mapping.DestinationName.Equals(existingMapping.DestinationName)) {
                return true;
            }
        }
        return false;
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
        if (!TryExtractGenericName(node.Expression, 2, out var genericName)) {
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

        return new(sourceSymbol,
            GetAllPropertySymbols(sourceSymbol),
            destinationSymbol,
            GetAllPropertySymbols(destinationSymbol, writableOnly: true),
            GetAllPublicConstructors(destinationSymbol),
            projectionOnly,
            GetCustomMappings(invocation.Origin),
            MapDefinition.CollectionMapping.NoCollection);
    }

    private static Dictionary<string, MappingCustomization> GetCustomMappings(InvocationExpressionSyntax node) {
        var invocation = node;
        var customMappings = new Dictionary<string, MappingCustomization>();

        while (invocation?.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: "ForMember" } memberExpr) {
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
                if (TryGetExplicitMapping(mapFromInvocation, out var lambdaExpr)) {
                    customMappings[destinationProp] = new MappingCustomization(lambdaMapping: lambdaExpr);
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

    private static bool TryGetExplicitMapping(InvocationExpressionSyntax mapFromInvocation, [NotNullWhen(true)] out SimpleLambdaExpressionSyntax? lambdaExpr) {
        if (mapFromInvocation.ArgumentList.Arguments[0].Expression is not SimpleLambdaExpressionSyntax lambda) {
            lambdaExpr = null;
            return false;
        }
        lambdaExpr = lambda;
        return true;
    }

    private static ITypeSymbol GetTypeSymbol(SemanticModel semanticModel, ExpressionSyntax sourceType, CancellationToken token)
        => semanticModel.GetTypeInfo(sourceType, token) is not { Type: ITypeSymbol sourceSymbol }
            ? throw new Exception("TODO: How did we get here??")
            : sourceSymbol;

    private static ImmutableArray<IPropertySymbol> GetAllPropertySymbols(ITypeSymbol? sourceSymbol, bool writableOnly = false) {
        var sourceProperties = new List<IPropertySymbol>();
        var trackedPropNames = new List<string>(); // This is to keep track of the property names so we don't add a duplicate (if derived class hides base member)
        while (sourceSymbol is not null) {
            var sourceMembers = sourceSymbol.GetMembers();
            for (var i = 0; i < sourceMembers.Length; i++) {
                var srcMember = sourceMembers[i];
                if (srcMember is IPropertySymbol srcProp && !trackedPropNames.Contains(srcProp.Name)) {
                    trackedPropNames.Add(srcProp.Name);
                    if (writableOnly && srcProp.SetMethod is not { DeclaredAccessibility: Accessibility.Public }) {
                        continue;
                    }
                    sourceProperties.Add(srcProp);
                }
            }

            sourceSymbol = sourceSymbol.BaseType;
        }

        return ImmutableArray.CreateRange(sourceProperties);
    }

    private static ImmutableArray<IMethodSymbol> GetAllPublicConstructors(ITypeSymbol? sourceSymbol) {
        var sourceCtors = new List<IMethodSymbol>();
        // var sourceCtorsWithParamCount = new Dictionary<int, List<IMethodSymbol>>();
        // var maxCtorParamCount = 0;
        if (sourceSymbol is not null) {
            var sourceMembers = sourceSymbol.GetMembers();
            for (var i = 0; i < sourceMembers.Length; i++) {
                var srcMember = sourceMembers[i];
                if (srcMember is IMethodSymbol { MethodKind: MethodKind.Constructor, DeclaredAccessibility: Accessibility.Public } ctorMethod) {
                    sourceCtors.Add(ctorMethod);
                }
                // if (srcMember is IMethodSymbol { MethodKind: MethodKind.Constructor, DeclaredAccessibility: Accessibility.Public } ctorMethod) {
                //     var paramCount = ctorMethod.Parameters.Length;
                //     if (!sourceCtorsWithParamCount.ContainsKey(paramCount)) {
                //         sourceCtorsWithParamCount[paramCount] = new();
                //     }
                //     sourceCtorsWithParamCount[paramCount].Add(ctorMethod);
                //     if (maxCtorParamCount < paramCount) {
                //         maxCtorParamCount = paramCount;
                //     }
                // }
            }

            // // Now order by highest param count
            // for (var i = maxCtorParamCount; i >= 0; i--) {
            //     if (sourceCtorsWithParamCount.TryGetValue(i, out var ctors)) {
            //         sourceCtors.AddRange(ctors);
            //     }
            // }
        }
        return ImmutableArray.CreateRange(sourceCtors);
    }

    private static bool TryExtractGenericName(ExpressionSyntax expression, int arity, [NotNullWhen(true)] out GenericNameSyntax? genericName) {
        while (expression is MemberAccessExpressionSyntax { Expression: InvocationExpressionSyntax invokeExpr }) {
            expression = invokeExpr.Expression;
        }
        if (expression is GenericNameSyntax { } matchedName) {
            if (matchedName.TypeArgumentList.Arguments.Count == arity) {
                genericName = matchedName;
                return true;
            }
        }
        if (expression is MemberAccessExpressionSyntax { Name: GenericNameSyntax { } matchedName2 }) {
            if (matchedName2.TypeArgumentList.Arguments.Count == arity) {
                genericName = matchedName2;
                return true;
            }
        }
        genericName = null;
        return false;
    }

    private static bool TryGetEnumerableTypeSymbol(ITypeSymbol typeSymbol, [NotNullWhen(true)] out INamedTypeSymbol? symbol) => TryGetGenericEnumerableTypeSymbol(typeSymbol, "IEnumerable", out symbol);

    private static bool TryGetSetTypeSymbol(ITypeSymbol typeSymbol, [NotNullWhen(true)] out INamedTypeSymbol? symbol) => TryGetGenericEnumerableTypeSymbol(typeSymbol, "ISet", out symbol);

    private static bool TryGetGenericEnumerableTypeSymbol(ITypeSymbol typeSymbol, string typeName, [NotNullWhen(true)] out INamedTypeSymbol? symbol) {
        if (typeSymbol is INamedTypeSymbol { Arity: 1 } namedSymbol && namedSymbol.Name == typeName && namedSymbol.ContainingNamespace.ToDisplayString() == "System.Collections.Generic") {
            symbol = namedSymbol;
            return true;
        }

        var interfaces = typeSymbol.AllInterfaces;
        for (var i = 0; i < interfaces.Length; i++) {
            if (interfaces[i] is { Arity: 1 } @interface && @interface.Name == typeName && @interface.ContainingNamespace.ToDisplayString() == "System.Collections.Generic") {
                symbol = @interface;
                return true;
            }
        }
        symbol = null;
        return false;
    }

    private record MapOrProjectionInvocation(InvocationExpressionSyntax Origin, GenericNameSyntax GenericName, IMethodSymbol Symbol, SemanticModel Semantic);
}
