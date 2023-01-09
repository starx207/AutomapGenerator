namespace AutomapGenerator.Generator.VerificationTests.Sources;
internal static class Profiles {
    public static string BASIC_CREATE_MAP = MapperWithMappings("BasicMapProfile", $"CreateMap<{SourceObj.NAME}, {DestinationObj.NAME}>();");
    public static string EMPTY_CONSTRUCTOR = MapperWithMappings("EmptyMapProfile");
    public const string NO_CONSTRUCTOR = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class EmptyMapProfile : MapProfile {
}";

    public const string ARROW_CONSTRUCTOR = $@"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {{
    public BasicMapProfile() => CreateMap<{SourceObj.NAME}, {DestinationObj.NAME}>();
}}";

    public static string MultipleSourcesToDestination(params string[] sources)
        => MapperWithMappings("BasicMapProfile", sources.Select(s => $"CreateMap<{s}, {DestinationObj.NAME}>();"));

    public static string SourceToMultipleDestinations(params string[] destinations) 
        => MapperWithMappings("BasicMapProfile", destinations.Select(d => $"CreateMap<{SourceObj.NAME}, {d}>();"));

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
