using System;
namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class SourceWithSinglePrefix : ISourceFile {
    public Guid TestId { get; set; }
    public string? TestType { get; set; }
    public DateTime? TestTimestamp { get; set; }
    public bool TestInUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
