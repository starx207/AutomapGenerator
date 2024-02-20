namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DestinationFromDeepNestedSrc : ISourceFile {
    public string? Level1Level2Description { get; set; }
    public string? ExplicitlyMappedValue { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
