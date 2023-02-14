namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;
internal static class Profiles {
    public static string CREATE_MAP_WITH_IGNORE = MapperWithMappings("IgnoreMemberProfile", $"CreateMap<{SourceObj.NAME}, {DestinationObj.NAME}>().ForMember(d => d.Type, opt => opt.Ignore());");
    public static string CREATE_MAP_WITH_MULTIPLE_IGNORES = MapperWithMappings("IgnoreMemberProfile",
        @$"
CreateMap<{SourceObj.NAME}, {DestinationObj.NAME}>()
    .ForMember(d => d.Type, opt => opt.Ignore())
    .ForMember(d => d.InUse, opt => opt.Ignore());");

    public static string CREATE_MAP_FOR_OBJ_BREAKS_WITH_CONVENTION = MapperWithMappings("ExplicitMemberMapProfile",
        @$"
CreateMap<{SourceObj.NAME}, {DestinationObj.NAME}>()
    .ForMember(
        dest => dest.StringProperty, 
        options => options.MapFrom(src => src.Type))
    .ForMember(
        d => d.HasTimestamp
        o => o.MapFrom(s => s.Timestamp.HasValue));");

    private static string MapperWithMappings(string mapperName, params string[] mappings) => MapperWithMappings(mapperName, mappings.AsEnumerable());
    private static string MapperWithMappings(string mapperName, IEnumerable<string> mappings) => $@"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class {mapperName} : MapProfile {{
    public {mapperName}() {{
        {string.Join(Environment.NewLine + "        ", mappings)}
    }}
}}";
}
