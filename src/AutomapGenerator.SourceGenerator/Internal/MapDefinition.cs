using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace AutomapGenerator.SourceGenerator.Internal;

internal record MapDefinition(string SourceName, ImmutableArray<IPropertySymbol> SourceProperties,
    string DestinationName, ImmutableArray<IPropertySymbol> WritableDestinationProperties, bool ProjectionOnly) {

    public List<(string destProp, string srcProp)> GetDestinationMappings() {
        var mappings = new List<(string, string)>();

        for (var i = 0; i < WritableDestinationProperties.Length; i++) {
            var destPropName = WritableDestinationProperties[i].Name;
            if (TryAddMatching(ref mappings, destPropName, p => p.Name == destPropName)) {
                continue;
            }
            if (TryAddMatching(ref mappings, destPropName, p => p.ContainingType.Name + p.Name == destPropName)) {
                continue;
            }
        }

        return mappings;
    }

    private bool TryAddMatching(ref List<(string, string)> mappings, string destPropName, Func<IPropertySymbol, bool> predicate) {
        var srcPropName = SourceProperties.SingleOrDefault(predicate)?.Name;
        if (srcPropName is not null) {
            mappings.Add((destPropName, srcPropName));
            return true;
        }
        return false;
    }
}
