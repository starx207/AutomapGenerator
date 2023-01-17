using AutomapGenerator.Generator.VerificationTests.Sources;

namespace AutomapGenerator.Generator.VerificationTests;

[UsesVerify]
public class MapperGenerator_CreateProjectionVerifications {
    private const string SNAPSHOT_LOCATION = @"Snapshots\CreateProjection";

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.BASIC_CREATE_PROJECTION
    }, SNAPSHOT_LOCATION);

    #endregion
}
