namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class OtherSimpleSourceObj : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
