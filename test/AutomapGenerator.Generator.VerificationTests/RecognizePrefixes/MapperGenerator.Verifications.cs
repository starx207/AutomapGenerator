using AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"RecognizePrefixes\Snapshots";

    #region Tests

    [Fact]
    public Task RecognizePrefixesOnTheSource() => Verifier.Verify(new[] {
        SourceObj.FullObjectWithPrefixes("TestPrefix", "Other", "Rest"),
        DestinationObj.FULL_OBJ_NO_PREFIX,
        Profiles.CreateMapperWithSourcePrefix(new[] {"TestPrefix", "Other", "Rest" })
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task KeepRecognizedPrefixesScopedToAProfile() => Verifier.Verify(new[] {
        SourceObj.FullObjectWithPrefixes("Test"),
        SourceObj.FullObjectWithNameAndPrefixes("SourceObjAlternate", "Test"),
        DestinationObj.FULL_OBJ_NO_PREFIX,
        Profiles.CreateMapperWithSourcePrefix("ProfileWithPrefixes", "Test"),
        Profiles.CreateMapperForSources("ProfileWithoutPrefixes", "SourceObjAlternate")
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task RecognizePrefixesOnTheSource_WhenFlattening() => Verifier.Verify(new[] {
        @"
namespace Verification.Models;
public class NestedObject {
    public string? TestDescription { get; set; }
    public string? OtherProp { get; set; }
}


public class SourceObject {
    public NestedObject TestChild { get; set; }
}",

        @"
namespace Verification.Models;
public class DestinationObject {
    public string? ChildDescription { get; set; }
    public string? ChildOtherProp { get; set; }
}",

        @"
using AutomapGenerator;
using Verification.Models;

namespace Verificaton.Mappings;
public class PrefixedFlatMapper : MapProfile {
    public PrefixedFlatMapper() {
        RecognizePrefixes(""Test"");
        CreateMap<SourceObject, DestinationObject>();
    }
}"
    }, SNAPSHOT_LOCATION);

    #endregion
}
