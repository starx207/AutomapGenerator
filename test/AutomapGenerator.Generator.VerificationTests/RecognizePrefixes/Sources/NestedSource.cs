namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class NestedSource : ISourceFile {
    public string? TestDescription { get; set; }
    public string? OtherProp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
