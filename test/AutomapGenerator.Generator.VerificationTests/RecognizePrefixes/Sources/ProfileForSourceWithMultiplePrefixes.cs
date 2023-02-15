namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class ProfileForSourceWithMultiplePrefixes : MapProfile, ISourceFile {
    public ProfileForSourceWithMultiplePrefixes() {
        RecognizePrefixes("TestPrefix", "Other", "Rest");
        CreateMap<SourceWithMultiplePrefixes, UnprefixedObject>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
