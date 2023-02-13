namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;
internal static class Profiles {
    public static string BASIC_CREATE_PROJECTION = MapperWithMappings("BasicMapProfile", $"CreateProjection<{SourceObj.NAME}, {DestinationObj.NAME}>();");

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
