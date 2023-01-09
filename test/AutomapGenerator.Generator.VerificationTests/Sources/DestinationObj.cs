namespace AutomapGenerator.Generator.VerificationTests.Sources;
internal static class DestinationObj {
    public const string NAME = "DestinationObj";

    public const string FULL_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
    public DateTime? Timestamp {{ get; set; }}
    public bool InUse {{ get; set; }}
}}";

    public const string FULL_OBJ_WITH_READONLY_PROP = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; }}
    public DateTime? Timestamp {{ get; set; }}
    public bool InUse {{ get; set; }}
}}";

    public const string SIMPLE_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
}}";

    public static string SimpleObjWithName(string name) => SIMPLE_OBJ.Replace(NAME, name);
}
