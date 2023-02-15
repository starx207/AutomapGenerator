namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class MultipleSourcesToDestinationProfile : MapProfile, ISourceFile {
    public MultipleSourcesToDestinationProfile() {
        CreateMap<SimpleSourceObj, SimpleDestinationObj>();
        CreateMap<OtherSimpleSourceObj, SimpleDestinationObj>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
