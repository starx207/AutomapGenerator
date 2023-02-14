namespace AutomapGenerator.SourceGenerator.Internal;
internal readonly struct MappingCustomization {
    public bool Ignore { get; }
    public char[] ExplicitMapping { get; } = Array.Empty<char>();

    public MappingCustomization() { }
    public MappingCustomization(bool ignore) => Ignore = ignore;
    public MappingCustomization(char[] explicitMapping) => ExplicitMapping = explicitMapping;
}
