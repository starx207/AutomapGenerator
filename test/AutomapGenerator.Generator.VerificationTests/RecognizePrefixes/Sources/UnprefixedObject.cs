using System;
namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;
public class UnprefixedObject : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
