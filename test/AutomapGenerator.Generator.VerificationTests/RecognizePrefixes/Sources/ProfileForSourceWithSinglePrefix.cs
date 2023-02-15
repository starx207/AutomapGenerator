namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class ProfileForSourceWithSinglePrefix : MapProfile, ISourceFile {
    public ProfileForSourceWithSinglePrefix() {
        RecognizePrefixes("Test");
        CreateMap<SourceWithSinglePrefix, UnprefixedObject>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
