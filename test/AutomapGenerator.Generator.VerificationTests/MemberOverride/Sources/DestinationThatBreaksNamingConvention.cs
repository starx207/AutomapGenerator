namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

public class DestinationThatBreaksNamingConvention : ISourceFile {
    public string? StringProperty { get; set; }
    public bool HasTimestamp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
