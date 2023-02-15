using AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources;

namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"RecognizePrefixes\Snapshots";

    #region Tests

    [Fact]
    public Task RecognizePrefixesOnTheSource() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SourceWithMultiplePrefixes>(),
        SourceReader.GetSourceFor<UnprefixedObject>(),
        SourceReader.GetSourceFor<ProfileForSourceWithMultiplePrefixes>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task KeepRecognizedPrefixesScopedToAProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<OtherSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<UnprefixedObject>(),
        SourceReader.GetSourceFor<ProfileForSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<ProfileWithoutRecognizedPrefixes>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task RecognizePrefixesOnTheSource_WhenFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<NestedSource>(),
        SourceReader.GetSourceFor<SourceWithNestedObject>(),
        SourceReader.GetSourceFor<FlattenedDestination>(),
        SourceReader.GetSourceFor<PrefixedFlatMapperProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
