using System;

namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class SourceObj : ISourceFile {
    public int Id { get; set; }
    public string? Text { get; set; }
    public bool Flag { get; set; }
    public DateTime Timestamp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
