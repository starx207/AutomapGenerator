using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapDefinitionHelper {

    private static MapDefinition? ConvertToMapDefinition(InvocationExpressionSyntax node, Compilation compilation, CancellationToken token) {
        if (node.Expression is not GenericNameSyntax { TypeArgumentList.Arguments.Count: 2 } genericName) {
            return null;
        }

        var semanticModel = compilation.GetSemanticModel(genericName.SyntaxTree);
        if (semanticModel.GetSymbolInfo(genericName, token) is not { Symbol: IMethodSymbol symbol }) {
            return null;
        }

        var methodName = symbol.ToDisplayString();
        // TODO: If I add the reference, how could I make this better?
        if (!methodName.StartsWith("AutomapGenerator.MapProfile.CreateMap<")) {
            return null;
        }

        var sourceType = genericName.TypeArgumentList.Arguments[0];
        var destinationType = genericName.TypeArgumentList.Arguments[1];

        if (semanticModel.GetTypeInfo(sourceType, token) is not { Type: ITypeSymbol sourceSymbol }) {
            throw new Exception("TODO: How did we get here??");
        }
        if (semanticModel.GetTypeInfo(destinationType, token) is not { Type: ITypeSymbol destinationSymbol }) {
            throw new Exception("TODO: How did we get here??");
        }

        var sourceProperties = new List<IPropertySymbol>();
        var sourceMembers = sourceSymbol.GetMembers();
        for (var i = 0; i < sourceMembers.Length; i++) {
            var srcMember = sourceMembers[i];
            if (srcMember is IPropertySymbol srcProp) {
                sourceProperties.Add(srcProp);
            }
        }

        var destProperties = new List<IPropertySymbol>();
        var destMembers = destinationSymbol.GetMembers();
        for (var i = 0; i < destMembers.Length; i++) {
            var destMember = destMembers[i];
            if (destMember is IPropertySymbol { IsReadOnly: false } destProp) {
                destProperties.Add(destProp);
            }
        }

        return new(sourceSymbol.ToDisplayString(),
            ImmutableArray.CreateRange(sourceProperties),
            destinationSymbol.ToDisplayString(),
            ImmutableArray.CreateRange(destProperties));
    }

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
}
