namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;
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

    public const string SIMPLE_OBJ = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public Guid Id {{ get; set; }}
    public string? Type {{ get; set; }}
}}";

    public const string OBJ_BREAKS_WITH_CONVENTION = $@"
namespace SampleMappingConsumer.Models;

public class {NAME} {{
    public string? StringProperty {{ get; set; }}
    public bool HasTimestamp {{ get; set; }}
}}";
}
