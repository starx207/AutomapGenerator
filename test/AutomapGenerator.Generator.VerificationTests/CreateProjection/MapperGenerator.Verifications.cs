using AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;

namespace AutomapGenerator.Generator.VerificationTests.CreateProjection;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateProjection\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<ProjectionSource>(),
        SourceReader.GetSourceFor<ProjectionDestination>(),
        SourceReader.GetSourceFor<ProjectionProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
