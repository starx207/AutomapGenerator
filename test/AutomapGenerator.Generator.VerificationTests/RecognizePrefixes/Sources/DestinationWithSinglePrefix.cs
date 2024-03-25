using System;
namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class DestinationWithSinglePrefix : ISourceFile {
    public Guid DtoId { get; set; }
    public string? DtoType { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
