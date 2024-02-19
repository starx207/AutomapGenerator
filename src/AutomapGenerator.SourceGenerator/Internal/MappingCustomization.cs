namespace AutomapGenerator.SourceGenerator.Internal;
internal readonly struct MappingCustomization {
    public bool Ignore { get; }
    public char[] ExplicitMapping { get; } = Array.Empty<char>();
    public bool ConstFallback { get; }

    public char[] FallbackMapping { get; } = Array.Empty<char>();


    public MappingCustomization() { }
    public MappingCustomization(bool ignore) => Ignore = ignore;
    public MappingCustomization(char[] explicitMapping, char[]? fallbackMapping, bool constFallback) {
        ExplicitMapping = explicitMapping;
        ConstFallback = constFallback;
        if (fallbackMapping is not null) {
            FallbackMapping = fallbackMapping;
        }
    }

}
