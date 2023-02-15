namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class FlattenedDestination : ISourceFile {
    public string? ChildDescription { get; set; }
    public string? ChildOtherProp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
