namespace AutomapGenerator.Generator.VerificationTests.CreateProjection;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("CreateProjection", "Snapshots");

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ProjectionSource>(),
        SourceReader.GetSourceFor<Sources.ProjectionDestination>(),
        SourceReader.GetSourceFor<Sources.ProjectionProfile>()
    }, _snapshotLocation);

    #endregion
}
