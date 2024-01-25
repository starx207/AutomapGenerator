namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class NestedSource : ISourceFile {
    public string? Value { get; set; }
    public string? OtherValue { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
