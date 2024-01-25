namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class ProfileWithFallbackForNested : MapProfile, ISourceFile {
    public ProfileWithFallbackForNested()
        => CreateMap<SourceObj, DestinationObjFromNested>()
        .ForMember(d => d.ChildObjValue, o => o.NullFallback("something else"))
        .ForMember(d => d.ChildObjOtherValue, o => o.NullFallback(s => s.OtherNullableString))
        .ForMember(d => d.NonNullString, o => o.MapFrom(s => s.ChildObj!.Value, "my default"))
        .ForMember(d => d.NullableString, o => o.MapFrom(s => s.ChildObj!.OtherValue, s => s.ChildObj!.Value));

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
