namespace AutomapGenerator.Generator.VerificationTests.CreateMap;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateMap\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.BasicMapProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleDestinationMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.OtherSimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.SourceToMultipleDestinationsProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleSourceMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.OtherSimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.MultipleSourcesToDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapperWithEmptyConstructor() => Verifier.Verify(SourceReader.GetSourceFor<Sources.EmptyConstructorProfile>(), SNAPSHOT_LOCATION, expectNoOutput: true);

    [Fact]
    public Task MapperWithNoConstructor() => Verifier.Verify(SourceReader.GetSourceFor<Sources.NoConstructorProfile>(), SNAPSHOT_LOCATION, expectNoOutput: true);

    [Fact]
    public Task SimpleObjectMap_WithArrowFunction() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.ArrowConstructorProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlyDestinations() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationWithReadonlyProp>(),
        SourceReader.GetSourceFor<Sources.MapWithReadonlyDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlySources() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithReadonlyProp>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.MapWithReadonlySourceProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectPrefixes() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationWithSrcPrefix>(),
        SourceReader.GetSourceFor<Sources.PrefixedDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.SourceObjWithNesting>(),
        SourceReader.GetSourceFor<Sources.DestinationFromNestedSrc>(),
        SourceReader.GetSourceFor<Sources.FlatteningProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
