namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class DestinationObj : ISourceFile {
    public string MappedString { get; set; } = string.Empty;
    public string? NullableMappedString { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
