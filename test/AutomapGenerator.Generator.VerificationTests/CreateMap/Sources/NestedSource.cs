namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class NestedSource : ISourceFile {
    public string? Description { get; set; }
    public string? OtherProp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
