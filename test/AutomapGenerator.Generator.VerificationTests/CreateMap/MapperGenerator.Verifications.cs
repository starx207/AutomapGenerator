using AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateMap\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<FullDestinationObj>(),
        SourceReader.GetSourceFor<BasicMapProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleDestinationMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SimpleSourceObj>(),
        SourceReader.GetSourceFor<SimpleDestinationObj>(),
        SourceReader.GetSourceFor<OtherSimpleDestinationObj>(),
        SourceReader.GetSourceFor<SourceToMultipleDestinationsProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleSourceMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SimpleSourceObj>(),
        SourceReader.GetSourceFor<OtherSimpleSourceObj>(),
        SourceReader.GetSourceFor<SimpleDestinationObj>(),
        SourceReader.GetSourceFor<MultipleSourcesToDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapperWithEmptyConstructor() => Verifier.Verify(SourceReader.GetSourceFor<EmptyConstructorProfile>(), SNAPSHOT_LOCATION);

    [Fact]
    public Task MapperWithNoConstructor() => Verifier.Verify(SourceReader.GetSourceFor<NoConstructorProfile>(), SNAPSHOT_LOCATION);

    [Fact]
    public Task SimpleObjectMap_WithArrowFunction() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<FullDestinationObj>(),
        SourceReader.GetSourceFor<ArrowConstructorProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlyDestinations() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<DestinationWithReadonlyProp>(),
        SourceReader.GetSourceFor<MapWithReadonlyDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlySources() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SourceWithReadonlyProp>(),
        SourceReader.GetSourceFor<FullDestinationObj>(),
        SourceReader.GetSourceFor<MapWithReadonlySourceProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectPrefixes() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<DestinationWithSrcPrefix>(),
        SourceReader.GetSourceFor<PrefixedDestinationProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<NestedSource>(),
        SourceReader.GetSourceFor<SourceObjWithNesting>(),
        SourceReader.GetSourceFor<DestinationFromNestedSrc>(),
        SourceReader.GetSourceFor<FlatteningProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
