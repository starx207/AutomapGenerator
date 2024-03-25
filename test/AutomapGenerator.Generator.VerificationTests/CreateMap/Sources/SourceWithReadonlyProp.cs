using System;
namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class SourceWithReadonlyProp : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
