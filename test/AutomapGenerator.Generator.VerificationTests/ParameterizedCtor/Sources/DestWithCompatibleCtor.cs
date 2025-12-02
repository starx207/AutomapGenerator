using System;

namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class DestWithCompatibleCtor : ISourceFile {
    public DestWithCompatibleCtor(string? text) => Text = text;

    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime Timestamp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
