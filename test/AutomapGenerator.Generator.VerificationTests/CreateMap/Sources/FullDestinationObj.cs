using System;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class FullDestinationObj : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
