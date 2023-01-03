using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace AutomapGenerator.SourceGenerator.Internal;

internal record MapDefinition(string SourceName, ImmutableArray<IPropertySymbol> SourceProperties,
    string DestinationName, ImmutableArray<IPropertySymbol> WritableDestinationProperties);
