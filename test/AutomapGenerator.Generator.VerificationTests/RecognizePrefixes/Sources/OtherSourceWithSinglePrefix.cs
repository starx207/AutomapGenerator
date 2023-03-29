﻿namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class OtherSourceWithSinglePrefix : ISourceFile {
    public Guid TestId { get; set; }
    public string? TestType { get; set; }
    public DateTime? TestTimestamp { get; set; }
    public bool TestInUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
