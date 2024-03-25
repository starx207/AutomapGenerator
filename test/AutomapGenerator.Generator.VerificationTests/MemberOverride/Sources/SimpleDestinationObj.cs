using System;

namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

public class SimpleDestinationObj : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
