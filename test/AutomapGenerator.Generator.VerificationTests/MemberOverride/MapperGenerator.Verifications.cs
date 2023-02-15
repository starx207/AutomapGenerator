using AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

namespace AutomapGenerator.Generator.VerificationTests.MemberOverride;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"MemberOverride\Snapshots";

    #region Tests

    [Fact]
    public Task IgnoreUnmappableProperty() => Verifier.Verify(new[] {
        SourceObj.SIMPLE_OBJ,
        DestinationObj.SIMPLE_OBJ,
        Profiles.CREATE_MAP_WITH_IGNORE
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleIgnoredProperties() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.CREATE_MAP_WITH_MULTIPLE_IGNORES
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task ExplicitMemberMapping() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.OBJ_BREAKS_WITH_CONVENTION,
        Profiles.CREATE_MAP_FOR_OBJ_BREAKS_WITH_CONVENTION
    }, SNAPSHOT_LOCATION);

    #endregion
}
