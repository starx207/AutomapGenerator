namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class SourceWithNullableValueTypes : ISourceFile {
    public string? NullableRefType { get; set; }
    public string NonNullableRefType { get; set; } = string.Empty;

    public int? NullableValueType { get; set; }
    public int NonNullableValueType { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
