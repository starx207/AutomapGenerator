namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"RecognizePrefixes\Snapshots";

    #region Tests

    [Fact]
    public Task RecognizePrefixesOnTheSource() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithMultiplePrefixes>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.ProfileForSourceWithMultiplePrefixes>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task RecognizePrefixesOnTheDestination() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.DestinationWithMultiplePrefixes>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.ProfileForDestinationWithMultiplePrefixes>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task KeepRecognizedPrefixesScopedToAProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.OtherSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.DestinationWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.ProfileForSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.ProfileWithoutRecognizedPrefixes>(),
        SourceReader.GetSourceFor<Sources.ProfileForDestinationWithSinglePrefix>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task RecognizePrefixesOnTheSource_WhenFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.SourceWithNestedObject>(),
        SourceReader.GetSourceFor<Sources.FlattenedDestination>(),
        SourceReader.GetSourceFor<Sources.PrefixedFlatMapperProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
