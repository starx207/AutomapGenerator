namespace AutomapGenerator.Generator.VerificationTests.Inheritance;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("Inheritance", "Snapshots");

    #region Tests

    [Fact]
    public Task MappingASourceWithInheritedBase() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.DerivedSource>(),
        SourceReader.GetSourceFor<Sources.Destination>(),
        SourceReader.GetSourceFor<Sources.DerivedSourceProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MappingToADestinationWithInheritedBase() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.Source>(),
        SourceReader.GetSourceFor<Sources.DerivedDestination>(),
        SourceReader.GetSourceFor<Sources.DerivedDestinationProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MappingSourceAndDestinationWithInheritedBases() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.DerivedSource>(),
        SourceReader.GetSourceFor<Sources.DerivedDestination>(),
        SourceReader.GetSourceFor<Sources.AllDerivedProfile>()
    }, _snapshotLocation);

    #endregion
}
