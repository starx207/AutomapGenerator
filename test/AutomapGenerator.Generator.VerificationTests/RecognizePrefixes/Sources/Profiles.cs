namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;
internal static class Profiles {
    public static string CreateMapperWithSourcePrefix(string profileName, params string[] sourcePrefixes) => MapperWithPrefixes(profileName, SourceObj.NAME, DestinationObj.NAME, sourcePrefixes: sourcePrefixes);
    public static string CreateMapperWithSourcePrefix(string[] sourcePrefixes) => CreateMapperWithSourcePrefix(profileName: "MapperWithSrcPrefix", sourcePrefixes: sourcePrefixes);
    public static string CreateMapperForSources(string profileName, string sourceName) => MapperWithPrefixes(profileName, sourceName, DestinationObj.NAME, Array.Empty<string>());
    private static string MapperWithPrefixes(string mapperName, string sourceName, string destinationName, string[] sourcePrefixes) => $@"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class {mapperName} : MapProfile {{
    public {mapperName}() {{
        {(sourcePrefixes.Length == 0 ? "" : $"RecognizePrefixes({string.Join(", ", sourcePrefixes.Select(pre => $"\"{pre}\""))});")}
        CreateMap<{sourceName}, {destinationName}>();
    }}
}}";
}
