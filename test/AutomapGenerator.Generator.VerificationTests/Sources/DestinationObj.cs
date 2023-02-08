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

    public const string FULL_OBJ_WITH_SRC_PREFIX = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid {SourceObj.NAME}Id {{ get; set; }}
    public string? {SourceObj.NAME}Type {{ get; set; }}
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

    public const string SIMPLE_OBJ_FROM_NESTED = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? ChildObjDescription {{ get; set; }}
    public string? ChildObjOtherProp {{ get; set; }}
}}";

    public static string SimpleObjWithName(string name) => SIMPLE_OBJ.Replace(NAME, name);

    public static string NestedObjWithChildName(string childDescName) => SIMPLE_OBJ_FROM_NESTED.Replace("ChildDescription", childDescName);
}
