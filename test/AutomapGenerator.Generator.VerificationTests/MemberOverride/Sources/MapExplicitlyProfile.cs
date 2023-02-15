namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

public class MapExplicitlyProfile : MapProfile, ISourceFile {
    public MapExplicitlyProfile() 
        => CreateMap<FullSourceObj, DestinationThatBreaksNamingConvention>()
            .ForMember(
                dest => dest.StringProperty,
                options => options.MapFrom(src => src.Type))
            .ForMember(
                d => d.HasTimestamp,
                o => o.MapFrom(s => s.Timestamp.HasValue));

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
