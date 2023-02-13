using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace AutomapGenerator.SourceGenerator.Internal;

internal record MapDefinition(string SourceName, ImmutableArray<IPropertySymbol> SourceProperties,
    string DestinationName, ImmutableArray<IPropertySymbol> WritableDestinationProperties, bool ProjectionOnly, 
    Dictionary<string, MappingCustomization> CustomMappings) {

    public List<(string destProp, string srcProp)> GetDestinationMappings() {
        var mappings = new List<(string, string)>();

        for (var i = 0; i < WritableDestinationProperties.Length; i++) {
            var destPropName = WritableDestinationProperties[i].Name;
            if (IsIgnored(destPropName)) {
                continue;
            }
            if (TryAddMatching(ref mappings, destPropName, p => p.Name == destPropName)) {
                continue;
            }
            if (TryAddMatching(ref mappings, destPropName, p => p.ContainingType.Name + p.Name == destPropName)) {
                continue;
            }
            if (TryAddFlattened(ref mappings, destPropName)) {
                continue;
            }
        }

        return mappings;
    }

    private bool IsIgnored(string destPropName)
        => CustomMappings.TryGetValue(destPropName, out var mapping) && mapping.Ignore;

    private bool TryAddMatching(ref List<(string, string)> mappings, string destPropName, Func<IPropertySymbol, bool> predicate) {
        var srcPropName = SourceProperties.SingleOrDefault(predicate)?.Name;
        if (srcPropName is not null) {
            mappings.Add((destPropName, srcPropName));
            return true;
        }
        return false;
    }

    private bool TryAddFlattened(ref List<(string, string)> mappings, string destPropName) {
        var nameParts = SplitNameByConvention(destPropName);
        var matchedPath = FindPropertyPath(SourceProperties, nameParts);
        if (matchedPath is not null) {
            mappings.Add((destPropName, matchedPath));
            return true;
        }
        return false;
    }

    private static string? FindPropertyPath(IEnumerable<IPropertySymbol> properties, string[] path) {
        foreach (var property in properties) {
            if (TryMatchProperty(property, path, out var matchedLength)) {
                if (matchedLength == path.Length) {
                    return property.Name;
                }

                if (property.Type is INamedTypeSymbol nestedType) {
                    var nestedProperties = nestedType.GetMembers().OfType<IPropertySymbol>();
                    var nestedPath = FindPropertyPath(nestedProperties, path.Skip(matchedLength).ToArray());

                    if (nestedPath is not null) {
                        return $"{property.Name}.{nestedPath}";
                    }
                }
            }
        }

        return null;
    }

    private static bool TryMatchProperty(IPropertySymbol property, string[] path, out int matchedLength) {
        // See if the property name matches the path
        var propNameParts = SplitNameByConvention(property.Name);
        if (propNameParts.SequenceEqual(path.Take(propNameParts.Length))) {
            matchedLength = propNameParts.Length;
            return true;
        }

        // If the path starts with the containing type name, skip it and re-check for a match
        var propTypeParts = SplitNameByConvention(property.ContainingType.Name);
        if (propTypeParts.SequenceEqual(path.Take(propTypeParts.Length)) 
            && propNameParts.SequenceEqual(path.Skip(propTypeParts.Length).Take(propNameParts.Length))) {

            matchedLength = propTypeParts.Length + propNameParts.Length;
            return true;
        }

        matchedLength = 0;
        return false;
    }

    private static string[] SplitNameByConvention(string name) {
        var output = new List<string>();
        var currentWord = new char[name.Length];
        var j = 0;

        for (var i = 0; i < name.Length; i++) {
            if (char.IsUpper(name[i]) && i != 0) {
                output.Add(new string(currentWord, 0, j));
                j = 0;
            }
            currentWord[j++] = name[i];
        }
        output.Add(new string(currentWord, 0, j));
        return output.ToArray();
    }
}
