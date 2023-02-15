namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;
internal static class SourceObj {
    private const string PREFIX_PLACEHOLDER = "<<replace_prefix>>";
    public const string NAME = "SourceObj";

    private const string FULL_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid {PREFIX_PLACEHOLDER}_0Id {{ get; set; }}
    public string? {PREFIX_PLACEHOLDER}_1Type {{ get; set; }}
    public DateTime? {PREFIX_PLACEHOLDER}_2Timestamp {{ get; set; }}
    public bool {PREFIX_PLACEHOLDER}_3InUse {{ get; set; }}
}}
";

    public static readonly string FULL_OBJ_NO_PREFIX = FullObjectWithPrefixes();

    public static string FullObjectWithNameAndPrefixes(string name, params string[] prefixes) {
        var objSrc = FULL_OBJ.Replace(NAME, name);
        for (var i = 0; i < 4; i++) {
            var prefix = prefixes.Length > i ? prefixes[i] : (prefixes.LastOrDefault() ?? "");
            var placeholder = $"{PREFIX_PLACEHOLDER}_{i}";
            objSrc = objSrc.Replace(placeholder, prefix);
        }
        return objSrc;
    }

    public static string FullObjectWithPrefixes(params string[] prefixes) => FullObjectWithNameAndPrefixes(NAME, prefixes);
}
