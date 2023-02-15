namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

public class PrefixedFlatMapperProfile : MapProfile, ISourceFile {
    public PrefixedFlatMapperProfile() {
        RecognizePrefixes("Test");
        CreateMap<SourceWithNestedObject, FlattenedDestination>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
