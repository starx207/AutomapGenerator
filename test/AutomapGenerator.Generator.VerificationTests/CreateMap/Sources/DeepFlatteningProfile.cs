namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DeepFlatteningProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public DeepFlatteningProfile() {
        CreateMap<SourceObjWithDeepNesting, DestinationFromDeepNestedSrc>()
            .ForMember(d => d.ExplicitlyMappedValue, o => o.MapFrom(s => s.Level1 != null && s.Level1.Level2 != null ? s.Level1.Level2.Description : null));
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
