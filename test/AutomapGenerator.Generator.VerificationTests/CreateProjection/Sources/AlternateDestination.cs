namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;
public class AlternateDestination : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
