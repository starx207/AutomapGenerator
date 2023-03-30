namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class ProfileForDestinationWithMultiplePrefixes : MapProfile, ISourceFile {
    public ProfileForDestinationWithMultiplePrefixes() {
        RecognizeDestinationPrefixes("Dto", "Other");
        CreateMap<UnprefixedObject, DestinationWithMultiplePrefixes>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
