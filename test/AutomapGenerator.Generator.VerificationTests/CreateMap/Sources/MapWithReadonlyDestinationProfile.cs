namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class MapWithReadonlyDestinationProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public MapWithReadonlyDestinationProfile() {
        CreateMap<FullSourceObj, DestinationWithReadonlyProp>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
