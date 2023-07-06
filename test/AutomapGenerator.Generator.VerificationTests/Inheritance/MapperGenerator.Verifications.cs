namespace AutomapGenerator.Generator.VerificationTests.Inheritance;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"Inheritance\Snapshots";

    #region Tests

    [Fact]
    public Task MappingASourceWithInheritedBase() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.DerivedSource>(),
        SourceReader.GetSourceFor<Sources.Destination>(),
        SourceReader.GetSourceFor<Sources.DerivedSourceProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MappingToADestinationWithInheritedBase() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.Source>(),
        SourceReader.GetSourceFor<Sources.DerivedDestination>(),
        SourceReader.GetSourceFor<Sources.DerivedDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MappingSourceAndDestinationWithInheritedBases() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.BaseClass>(),
        SourceReader.GetSourceFor<Sources.DerivedSource>(),
        SourceReader.GetSourceFor<Sources.DerivedDestination>(),
        SourceReader.GetSourceFor<Sources.AllDerivedProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
