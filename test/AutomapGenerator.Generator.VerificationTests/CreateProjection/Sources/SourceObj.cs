namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;
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

}
