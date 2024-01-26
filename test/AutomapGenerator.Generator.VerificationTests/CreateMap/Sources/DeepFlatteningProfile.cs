namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DeepFlatteningProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public DeepFlatteningProfile() {
        CreateMap<SourceObjWithDeepNesting, DestinationFromDeepNestedSrc>()
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .ForMember(d => d.ExplicitlyMappedValue, o => o.MapFrom(s => s.Level1.Level2.Description))
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .ForMember(d => d.OtherExplicitValue, o => o.MapFrom(s => s.Level1!.Level2!.Description));
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
