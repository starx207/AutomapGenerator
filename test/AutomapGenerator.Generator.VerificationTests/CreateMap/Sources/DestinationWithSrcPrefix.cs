namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DestinationWithSrcPrefix : ISourceFile {
    public Guid FullSourceObjId { get; set; }
    public string? FullSourceObjType { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
