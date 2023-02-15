namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class SourceWithNestedObject : ISourceFile {
    public NestedSource? TestChild { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
