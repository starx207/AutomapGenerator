namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class ProfileForDestinationWithSinglePrefix : MapProfile, ISourceFile {
    public ProfileForDestinationWithSinglePrefix() {
        RecognizeDestinationPrefixes("Dto");
        CreateMap<UnprefixedObject, DestinationWithSinglePrefix>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
