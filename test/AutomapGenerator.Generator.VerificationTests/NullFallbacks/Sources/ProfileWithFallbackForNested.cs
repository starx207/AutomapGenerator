namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class ProfileWithFallbackForNested : MapProfile, ISourceFile {
    public ProfileWithFallbackForNested()
        => CreateMap<SourceObj, DestinationObjFromNested>()
        .ForMember(d => d.ChildObjValue, o => o.MapFrom(s => s.ChildObj == null || s.ChildObj.Value == null ? "something else" : s.ChildObj.Value))
        .ForMember(d => d.ChildObjOtherValue, o => o.MapFrom(s => s.ChildObj == null || s.ChildObj.OtherValue == null ? s.OtherNullableString : s.ChildObj.OtherValue))
        .ForMember(d => d.NonNullString, o => o.MapFrom(s => s.ChildObj == null || s.ChildObj.Value == null ? "my default" : s.ChildObj.Value))
        .ForMember(d => d.NullableString, o => o.MapFrom(s => s.ChildObj == null || s.ChildObj.OtherValue == null ? (s.ChildObj == null ? null : s.ChildObj.Value) : s.ChildObj.OtherValue));

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
