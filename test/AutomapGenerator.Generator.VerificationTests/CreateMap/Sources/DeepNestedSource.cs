namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;
public class DeepNestedSource : ISourceFile {
    public NestedSource? Level2 { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
