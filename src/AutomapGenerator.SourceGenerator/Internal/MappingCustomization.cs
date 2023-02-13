namespace AutomapGenerator.SourceGenerator.Internal;
internal readonly struct MappingCustomization {
    public bool Ignore { get; }

    public MappingCustomization() { }
    public MappingCustomization(bool ignore) => Ignore = ignore;
}
