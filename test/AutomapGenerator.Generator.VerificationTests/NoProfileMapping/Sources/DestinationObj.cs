namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping.Sources;
public class DestinationObj : ISourceFile {
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
