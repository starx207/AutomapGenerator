namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DeepFlatteningProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public DeepFlatteningProfile() {
        CreateMap<SourceObjWithDeepNesting, DestinationFromDeepNestedSrc>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
