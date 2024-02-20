namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class ProfileWithStaticFallback : MapProfile, ISourceFile {
    public ProfileWithStaticFallback() 
        => CreateMap<SourceObj, DestinationObj>()
        .ForMember(d => d.MappedString, o => o.MapFrom(s => s.NullableString ?? "default value"))
        .ForMember(d => d.NullableMappedString, o => o.Ignore());

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
