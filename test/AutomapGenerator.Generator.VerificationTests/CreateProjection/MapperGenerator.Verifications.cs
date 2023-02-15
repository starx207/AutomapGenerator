namespace AutomapGenerator.Generator.VerificationTests.CreateProjection;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateProjection\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ProjectionSource>(),
        SourceReader.GetSourceFor<Sources.ProjectionDestination>(),
        SourceReader.GetSourceFor<Sources.ProjectionProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
