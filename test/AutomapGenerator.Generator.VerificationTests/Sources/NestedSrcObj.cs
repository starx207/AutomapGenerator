namespace AutomapGenerator.Generator.VerificationTests.Sources;
internal static class NestedSrcObj {
    public const string NAME = "NestedSrcObj";

    public const string FULL_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public string? Description {{ get; set; }}
    public string? OtherProp {{ get; set; }}
}}
";
}
