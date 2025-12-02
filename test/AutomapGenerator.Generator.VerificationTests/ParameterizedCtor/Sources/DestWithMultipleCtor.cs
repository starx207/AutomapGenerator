using System;

namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class DestWithMultipleCtor : ISourceFile {
    public DestWithMultipleCtor(byte[] somethingElse, string? text, DateTime timestamp) {
        Text = text;
        Timestamp = timestamp;
    }
    public DestWithMultipleCtor(string? text, byte[] somethingElse) => Text = text;
    public DestWithMultipleCtor() { }
    public DestWithMultipleCtor(string? text) => Text = text;
    public DestWithMultipleCtor(string? text, DateTime timestamp) {
        Text = text;
        Timestamp = timestamp;
    }

    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTime Timestamp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
