namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor;
public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("ParameterizedCtor", "Snapshots");

    #region Tests

    [Fact]
    public Task ObjectWithIncompatibleConstructor() => Verifier.Verify(new[] {
       SourceReader.GetSourceFor<Sources.SourceObj>(),
       SourceReader.GetSourceFor(new Sources.DestWithIncompatibleCtor([])),
       SourceReader.GetSourceFor<Sources.ProfileForIncompatibleCtor>()
    }, _snapshotLocation);

    #endregion
}
