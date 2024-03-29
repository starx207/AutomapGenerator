﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutomapGenerator.SourceGenerator.Internal;

internal class MapDefinition {
    private readonly List<Mapping> _allMappings = new();
    private readonly Dictionary<string, List<Mapping>> _mappingsBySource = new();

    public IReadOnlyList<Mapping> Mappings => _allMappings;
    public IReadOnlyDictionary<string, List<Mapping>> MappingsBySource => _mappingsBySource;
    public List<string> RecognizedSourcePrefixes { get; } = new();
    public List<string> RecognizedDestinationPrefixes { get; } = new();

    public void RemoveMappingAt(int index) {
        var mappingToRemove = _allMappings[index];
        _allMappings.Remove(mappingToRemove);
        _mappingsBySource[mappingToRemove.SourceName].Remove(mappingToRemove);
    }

    public void AddMappings(List<Mapping> mappings) {
        for (var i = 0; i < mappings.Count; i++) {
            AddMapping(mappings[i]);
        }
    }

    public void AddMapping(Mapping mapping) {
        _allMappings.Add(mapping);
        if (!_mappingsBySource.ContainsKey(mapping.SourceName)) {
            _mappingsBySource[mapping.SourceName] = new();
        }
        _mappingsBySource[mapping.SourceName].Add(mapping);
    }

    public List<(string destProp, string srcProp)> GetDestinationMappings(Mapping mapping, string sourceVarName) {
        var mappings = new List<(string, string)>();

        for (var i = 0; i < mapping.WritableDestinationProperties.Length; i++) {
            var destPropName = mapping.WritableDestinationProperties[i].Name;
            if (mapping.IsIgnored(destPropName)) {
                continue;
            }
            if (mapping.TryGetExplicitMapping(ref mappings, destPropName, sourceVarName)) {
                continue;
            }

            var mapped = false;
            for (var j = 0; j < RecognizedDestinationPrefixes.Count + 1; j++) {
                mapped = true;

                // First time through, we try with no prefix. Then we try again with each prefix
                var unprefixedDestPropName = destPropName;
                if (j > 0) {
                    var prefix = RecognizedDestinationPrefixes[j - 1];
                    if (!unprefixedDestPropName.StartsWith(prefix)) {
                        mapped = false; // Just in case this is our last loop iteration.
                        continue;
                    }
                    unprefixedDestPropName = unprefixedDestPropName.Substring(prefix.Length);
                }

                // Match the name exactly as-is
                if (mapping.TryAddMatching(ref mappings, destPropName, p => p.Name == unprefixedDestPropName, sourceVarName)) {
                    break;
                }
                // Match the name with the Source type-name as a prefix
                if (mapping.TryAddMatching(ref mappings, destPropName, p => p.ContainingType.Name + p.Name == unprefixedDestPropName, sourceVarName)) {
                    break;
                }
                // Match the name with one of the recoginzed source prefixes
                if (mapping.TryAddMatching(ref mappings, destPropName, p => RecognizedSourcePrefixes.Any(prefix => p.Name == prefix + unprefixedDestPropName), sourceVarName)) {
                    break;
                }
                mapped = false;
            }

            if (mapped) {
                continue;
            }
            if (mapping.TryAddFlattened(ref mappings, destPropName, RecognizedSourcePrefixes, RecognizedDestinationPrefixes, sourceVarName)) {
                continue;
            }
        }

        return mappings;
    }

    public record Mapping(ITypeSymbol SourceSymbol, ImmutableArray<IPropertySymbol> SourceProperties,
        ITypeSymbol DestinationSymbol, ImmutableArray<IPropertySymbol> WritableDestinationProperties, 
        ImmutableArray<IMethodSymbol> DestinationConstructors,
        bool ProjectionOnly, Dictionary<string, MappingCustomization> CustomMappings) {

        public string SourceName { get; } = SourceSymbol.ToDisplayString();
        public string DestinationName { get; } = DestinationSymbol.ToDisplayString();

        public bool TryGetExplicitMapping(ref List<(string, string)> mappings, string destPropName, string sourceVarName) {
            if (CustomMappings.TryGetValue(destPropName, out var customMapping) && customMapping.LambdaMapping is { } explicitLambdaExpr) {
                var oldParam = explicitLambdaExpr.Parameter;
                var newParam = SyntaxFactory.Parameter(SyntaxFactory.Identifier(sourceVarName));
                var rewriter = new ParameterRewriter(oldParam.Identifier, newParam.Identifier);
                var rewrittenBody = rewriter.Visit(explicitLambdaExpr.Body);
                mappings.Add((destPropName, rewrittenBody.ToFullString()));
                return true;
            }

            return false;
        }

        public bool IsIgnored(string destPropName)
            => CustomMappings.TryGetValue(destPropName, out var mapping) && mapping.Ignore;

        public bool TryAddMatching(ref List<(string, string)> mappings, string destPropName, Func<IPropertySymbol, bool> predicate, string sourceVarName) {
            var srcPropName = SourceProperties.SingleOrDefault(predicate)?.Name;
            if (srcPropName is not null) {
                mappings.Add((destPropName, $"{sourceVarName}.{srcPropName}"));
                return true;
            }
            return false;
        }

        public bool TryAddFlattened(ref List<(string, string)> mappings, string destPropName, IEnumerable<string> recognizedSourcePrefixes, List<string> recognizedDestinationPrefixes, string sourceVarName) {
            var nameParts = SplitNameByConvention(destPropName);
            var sourcePrefixPaths = recognizedSourcePrefixes.Select(SplitNameByConvention).ToArray();
            var matchedPath = FindPropertyPath(SourceProperties, nameParts, sourcePrefixPaths);
            var i = 0;

            while ((matchedPath is null) && i < recognizedDestinationPrefixes.Count) {
                if (nameParts[0] == recognizedDestinationPrefixes[i]) {
                    matchedPath = FindPropertyPath(
                        SourceProperties,
                        new ArraySegment<string>(nameParts, 1, nameParts.Length - 1),
                        sourcePrefixPaths);
                }
                i++;
            }

            if (matchedPath is not null) {
                mappings.Add((destPropName, $"{sourceVarName}.{matchedPath}"));
                return true;
            }
            return false;
        }

        private static string? FindPropertyPath(IEnumerable<IPropertySymbol> properties, string[] path, string[][] sourcePrefixPaths)
            => FindPropertyPath(properties, new ArraySegment<string>(path), sourcePrefixPaths);

        private static string? FindPropertyPath(IEnumerable<IPropertySymbol> properties, ArraySegment<string> path, string[][] sourcePrefixPaths) {
            foreach (var property in properties) {
                if (TryMatchProperty(property, path, sourcePrefixPaths, out var matchedLength)) {
                    if (matchedLength == path.Count) {
                        return property.Name;
                    }

                    if (property.Type is INamedTypeSymbol nestedType) {
                        var nestedProperties = nestedType.GetMembers().OfType<IPropertySymbol>();
                        var newOffset = path.Offset + matchedLength;
                        var nestedPath = FindPropertyPath(
                            nestedProperties,
                            new ArraySegment<string>(path.Array, newOffset, path.Array.Length - newOffset),
                            sourcePrefixPaths);

                        if (nestedPath is not null) {
                            var lastSection = nestedPath.Split('.', '?')[0];
                            var separator = nestedType.NullableAnnotation == NullableAnnotation.Annotated // Annotated as nullable
                                && ((!nestedType.IsValueType) || (lastSection != "HasValue" && lastSection != "Value")) // Is reference type, or nullable value type and not accessing the "Value" or "HasValue" property
                                ? "?." : ".";
                            return property.Name + separator + nestedPath;
                        }
                    }
                }
            }

            return null;
        }

        private static bool TryMatchProperty(IPropertySymbol property, ArraySegment<string> path, string[][] sourcePrefixPaths, out int matchedLength) {
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

        private static string[] SplitNameByConvention(string name) => SplitNameByConvention(name.AsSpan());

        private static string[] SplitNameByConvention(ReadOnlySpan<char> name) {
            var output = new List<string>();
            var currentWord = new char[name.Length];
            var j = 0;

            for (var i = 0; i < name.Length; i++) {
                if (char.IsUpper(name[i]) && i != 0) {
                    var offset = currentWord[j - 1] == '.' ? -1 : 0; // Don't include trailing "." (can happen when splitting an explicit mapping)
                    output.Add(new string(currentWord, 0, j + offset));
                    j = 0;
                }
                currentWord[j++] = name[i];
            }
            output.Add(new string(currentWord, 0, j));
            return output.ToArray();
        }
    }
}
