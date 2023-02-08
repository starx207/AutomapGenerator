namespace AutomapGenerator.Generator.VerificationTests.Sources;
internal static class SourceObj {
    public const string NAME = "SourceObj";

    public const string FULL_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
    public DateTime? Timestamp {{ get; set; }}
    public bool InUse {{ get; set; }}
}}
";

    public const string FULL_OBJ_WITH_READONLY_PROP = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
    public DateTime? Timestamp {{ get; set; }}
    public bool InUse {{ get; }}
}}
";

    public const string SIMPLE_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
}}
";

    public const string SIMPLE_OBJ_WITH_NESTING = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public {NestedSrcObj.NAME} ChildObj {{ get; set; }}
}}";

    public static string SimpleObjWithName(string name) => SIMPLE_OBJ.Replace(NAME, name);
}
