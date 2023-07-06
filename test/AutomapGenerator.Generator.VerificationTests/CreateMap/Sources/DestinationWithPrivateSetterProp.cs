namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DestinationWithPrivateSetterProp : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; private set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
