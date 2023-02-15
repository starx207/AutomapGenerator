using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal class MapDefinition {
    // TODO: Remove this if I don't end up using it. Or simplify to just keep the parts of it that I need
    public ClassDeclarationSyntax ClassDeclaration { get; }
    public List<Mapping> Mappings { get; } = new();
    public List<string> RecognizedSourcePrefixes { get; } = new();

    public MapDefinition(ClassDeclarationSyntax declaration) => ClassDeclaration = declaration;

    public List<(string destProp, string srcProp)> GetDestinationMappings(Mapping mapping) {
        var mappings = new List<(string, string)>();

        for (var i = 0; i < mapping.WritableDestinationProperties.Length; i++) {
            var destPropName = mapping.WritableDestinationProperties[i].Name;
            if (mapping.IsIgnored(destPropName)) {
                continue;
            }
            if (mapping.TryGetExplicitMapping(ref mappings, destPropName)) {
                continue;
            }
            // Match the name exactly as-is
            if (mapping.TryAddMatching(ref mappings, destPropName, p => p.Name == destPropName)) {
                continue;
            }
            // Match the name with the Source type-name as a prefix
            if (mapping.TryAddMatching(ref mappings, destPropName, p => p.ContainingType.Name + p.Name == destPropName)) {
                continue;
            }
            // Match the name with one of the recoginzed source prefixes
            if (mapping.TryAddMatching(ref mappings, destPropName, p => RecognizedSourcePrefixes.Any(prefix => p.Name == prefix + destPropName))) {
                continue;
            }
            if (mapping.TryAddFlattened(ref mappings, destPropName, RecognizedSourcePrefixes)) {
                continue;
            }
        }

        return mappings;
    }

    public record Mapping(string SourceName, ImmutableArray<IPropertySymbol> SourceProperties,
        string DestinationName, ImmutableArray<IPropertySymbol> WritableDestinationProperties, bool ProjectionOnly,
        Dictionary<string, MappingCustomization> CustomMappings) {

        public bool TryGetExplicitMapping(ref List<(string, string)> mappings, string destPropName) {
            if (CustomMappings.TryGetValue(destPropName, out var customMapping) && customMapping.ExplicitMapping.Length > 0) {
                mappings.Add((destPropName, new string(customMapping.ExplicitMapping)));
                return true;
            }
            return false;
        }

        public bool IsIgnored(string destPropName)
            => CustomMappings.TryGetValue(destPropName, out var mapping) && mapping.Ignore;

        public bool TryAddMatching(ref List<(string, string)> mappings, string destPropName, Func<IPropertySymbol, bool> predicate) {
            var srcPropName = SourceProperties.SingleOrDefault(predicate)?.Name;
            if (srcPropName is not null) {
                mappings.Add((destPropName, srcPropName));
                return true;
            }
            return false;
        }

        public bool TryAddFlattened(ref List<(string, string)> mappings, string destPropName, IEnumerable<string> recognizedSourcePrefixes) {
            var nameParts = SplitNameByConvention(destPropName);
            var sourcePrefixPaths = recognizedSourcePrefixes.Select(SplitNameByConvention).ToArray();
            var matchedPath = FindPropertyPath(SourceProperties, nameParts, sourcePrefixPaths);
            if (matchedPath is not null) {
                mappings.Add((destPropName, matchedPath));
                return true;
            }
            return false;
        }

        private static string? FindPropertyPath(IEnumerable<IPropertySymbol> properties, string[] path, string[][] sourcePrefixPaths) {
            foreach (var property in properties) {
                if (TryMatchProperty(property, path, sourcePrefixPaths, out var matchedLength)) {
                    if (matchedLength == path.Length) {
                        return property.Name;
                    }

                    if (property.Type is INamedTypeSymbol nestedType) {
                        var nestedProperties = nestedType.GetMembers().OfType<IPropertySymbol>();
                        var nestedPath = FindPropertyPath(nestedProperties, path.Skip(matchedLength).ToArray(), sourcePrefixPaths);

                        if (nestedPath is not null) {
                            return $"{property.Name}.{nestedPath}";
                        }
                    }
                }
            }

            return null;
        }

        private static bool TryMatchProperty(IPropertySymbol property, string[] path, string[][] sourcePrefixPaths, out int matchedLength) {
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


            // If the propName starts with one of the source prefixes, skip it and re-check for a match
            for (var i = 0; i < sourcePrefixPaths.Length; i++) {
                var checkPrefix = sourcePrefixPaths[i];
                if (!checkPrefix.SequenceEqual(propNameParts.Take(checkPrefix.Length))) {
                    continue;
                }

                var unprefixedPropNameParts = propNameParts.Skip(checkPrefix.Length).ToArray();
                if (unprefixedPropNameParts.SequenceEqual(path.Take(unprefixedPropNameParts.Length))) {
                    matchedLength = unprefixedPropNameParts.Length;
                    return true;
                }
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
}
