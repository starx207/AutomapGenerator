namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class DestinationObjFromNested : ISourceFile {
    public string ChildObjValue { get; set; } = string.Empty;
    public string NonNullString { get; set; } = string.Empty;
    public string? ChildObjOtherValue { get; set; }
    public string? NullableString { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
