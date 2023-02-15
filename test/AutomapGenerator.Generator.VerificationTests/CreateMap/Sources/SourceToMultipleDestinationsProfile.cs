namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class SourceToMultipleDestinationsProfile : MapProfile, ISourceFile {
    public SourceToMultipleDestinationsProfile() {
        CreateMap<SimpleSourceObj, SimpleDestinationObj>();
        CreateMap<SimpleSourceObj, OtherSimpleDestinationObj>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
