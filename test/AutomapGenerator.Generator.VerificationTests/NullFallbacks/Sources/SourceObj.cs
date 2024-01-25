namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class SourceObj : ISourceFile {
    public string? NullableString { get; set; }
    public string? OtherNullableString { get; set; }
    public string NonNullString { get; set; } = string.Empty;
    public NestedSource? ChildObj { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
