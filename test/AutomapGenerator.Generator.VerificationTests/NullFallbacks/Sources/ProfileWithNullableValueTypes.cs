namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources;
public class ProfileWithNullableValueTypes : MapProfile, ISourceFile {
    public ProfileWithNullableValueTypes()
        => CreateMap<SourceWithNullableValueTypes, DestinationForNullableValueTypes>()
        .ForMember(d => d.NonNullableRefSimpleMap, o => o.MapFrom(s => s.NonNullableRefType))
        .ForMember(d => d.NonNullableRefMapWithStaticFallback, o => o.MapFrom(s => s.NullableRefType ?? "test"))
        .ForMember(d => d.NonNullableRefMapWithPropertyFallback, o => o.MapFrom(s => s.NullableRefType ?? s.NonNullableRefType))
        .ForMember(d => d.NonNullableRefIgnored, o => o.Ignore())

        .ForMember(d => d.NullableRefSimpleMap, o => o.MapFrom(s => s.NullableRefType))
        .ForMember(d => d.NullableRefMapWithStaticFallback, o => o.MapFrom(s => s.NullableRefType ?? "test"))
        .ForMember(d => d.NullableRefMapWithPropertyFallback, o => o.MapFrom(s => s.NullableRefType ?? s.NonNullableRefType))
        .ForMember(d => d.NullableRefIgnored, o => o.Ignore())

        .ForMember(d => d.NonNullableValueSimpleMap, o => o.MapFrom(s => s.NonNullableValueType))
        .ForMember(d => d.NonNullableValueMapWithStaticFallback, o => o.MapFrom(s => s.NullableValueType ?? 99))
        .ForMember(d => d.NonNullableValueMapWithPropertyFallback, o => o.MapFrom(s => s.NullableValueType ?? s.NonNullableValueType))
        .ForMember(d => d.NonNullableValueIgnored, o => o.Ignore())

        .ForMember(d => d.NullableValueSimpleMap, o => o.MapFrom(s => s.NullableValueType))
        .ForMember(d => d.NullableValueMapWithStaticFallback, o => o.MapFrom(s => s.NullableValueType ?? 99))
        .ForMember(d => d.NullableValueMapWithPropertyFallback, o => o.MapFrom(s => s.NullableValueType ?? s.NonNullableValueType))
        .ForMember(d => d.NullableValueIgnored, o => o.Ignore())
        ;

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
