using AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;

namespace AutomapGenerator.Generator.VerificationTests.CreateProjection;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateProjection\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.BASIC_CREATE_PROJECTION
    }, SNAPSHOT_LOCATION);

    #endregion
}
