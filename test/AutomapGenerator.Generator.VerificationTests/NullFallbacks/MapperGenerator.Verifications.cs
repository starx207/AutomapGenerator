namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks;

[UsesVerify]
public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("NullFallbacks", "Snapshots");

    #region Tests

    [Fact]
    public Task MappingNullableSource_WithStaticFallback() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ProfileWithStaticFallback>()
    }, _snapshotLocation);

    [Fact]
    public Task MappingNullableSource_WithAlternateMapFallback() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ProfileWithAlternateMapFallback>()
    }, _snapshotLocation);

    [Fact]
    public Task MappingNestedSource_WithNullFallback() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ProfileWithFallbackForNested>()
    }, _snapshotLocation);

    #endregion
}
