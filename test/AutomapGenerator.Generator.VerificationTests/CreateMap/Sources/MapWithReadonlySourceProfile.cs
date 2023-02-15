namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class MapWithReadonlySourceProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public MapWithReadonlySourceProfile() {
        CreateMap<SourceWithReadonlyProp, FullDestinationObj>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
