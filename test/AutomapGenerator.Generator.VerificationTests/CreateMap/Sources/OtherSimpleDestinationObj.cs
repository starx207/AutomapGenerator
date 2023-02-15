﻿namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class OtherSimpleDestinationObj : ISourceFile {
    public Guid Id { get; set; }
    public string? Type { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
