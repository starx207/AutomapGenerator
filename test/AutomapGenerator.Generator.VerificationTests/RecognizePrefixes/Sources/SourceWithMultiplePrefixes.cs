using System;
namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class SourceWithMultiplePrefixes : ISourceFile {
    public Guid TestPrefixId { get; set; }
    public string? OtherType { get; set; }
    public DateTime? RestTimestamp { get; set; }
    public bool RestInUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
