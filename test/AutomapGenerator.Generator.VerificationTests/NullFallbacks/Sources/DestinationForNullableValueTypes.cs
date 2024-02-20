namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class DestinationForNullableValueTypes : ISourceFile {
    public string NonNullableRefSimpleMap { get; set; } = string.Empty;
    public string NonNullableRefMapWithStaticFallback { get; set; } = string.Empty;
    public string NonNullableRefMapWithPropertyFallback { get; set; } = string.Empty;
    public string NonNullableRefIgnored { get; set; } = string.Empty;

    public string? NullableRefSimpleMap { get; set; }
    public string? NullableRefMapWithStaticFallback { get; set; }
    public string? NullableRefMapWithPropertyFallback { get; set; }
    public string? NullableRefIgnored { get; set; }

    public int NonNullableValueSimpleMap { get; set; }
    public int NonNullableValueMapWithStaticFallback { get; set; }
    public int NonNullableValueMapWithPropertyFallback { get; set; }
    public int NonNullableValueIgnored { get; set; }

    public int? NullableValueSimpleMap { get; set; }
    public int? NullableValueMapWithStaticFallback { get; set; }
    public int? NullableValueMapWithPropertyFallback { get; set; }
    public int? NullableValueIgnored { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
