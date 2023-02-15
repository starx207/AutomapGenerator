namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class FlatteningProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public FlatteningProfile() {
        CreateMap<SourceObjWithNesting, DestinationFromNestedSrc>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
