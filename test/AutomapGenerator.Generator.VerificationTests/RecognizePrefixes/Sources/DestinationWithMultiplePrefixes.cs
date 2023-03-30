namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class DestinationWithMultiplePrefixes : ISourceFile {
    public Guid DtoId { get; set; }
    public string? OtherType { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
