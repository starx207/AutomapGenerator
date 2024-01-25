namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class ProfileWithAlternateMapFallback : MapProfile, ISourceFile {
    public ProfileWithAlternateMapFallback() 
        => CreateMap<SourceObj, DestinationObj>()
        .ForMember(d => d.MappedString, o => o.MapFrom(s => s.NullableString, s => s.NonNullString))
        .ForMember(d => d.NullableMappedString, o => o.MapFrom(s => s.NullableString, s => s.OtherNullableString));

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
