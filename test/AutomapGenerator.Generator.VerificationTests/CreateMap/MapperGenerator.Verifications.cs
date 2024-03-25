using System.IO;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("CreateMap", "Snapshots");

    #region Tests

    [Fact]
    public Task SimpleObjectMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.BasicMapProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MultipleDestinationMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.OtherSimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.SourceToMultipleDestinationsProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MultipleSourceMap() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.OtherSimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.MultipleSourcesToDestinationProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapperWithEmptyConstructor() => Verifier.Verify(SourceReader.GetSourceFor<Sources.EmptyConstructorProfile>(), _snapshotLocation, expectNoOutput: true);

    [Fact]
    public Task MapperWithNoConstructor() => Verifier.Verify(SourceReader.GetSourceFor<Sources.NoConstructorProfile>(), _snapshotLocation, expectNoOutput: true);

    [Fact]
    public Task SimpleObjectMap_WithArrowFunction() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.ArrowConstructorProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithReadonlyDestinations() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationWithReadonlyProp>(),
        SourceReader.GetSourceFor<Sources.MapWithReadonlyDestinationProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithPrivateSetterDestinations() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationWithPrivateSetterProp>(),
        SourceReader.GetSourceFor<Sources.MapWithPrivateSetterDestinationProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithReadonlySources() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithReadonlyProp>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.MapWithReadonlySourceProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithSourceObjectPrefixes() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationWithSrcPrefix>(),
        SourceReader.GetSourceFor<Sources.PrefixedDestinationProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithSourceObjectFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.SourceObjWithNesting>(),
        SourceReader.GetSourceFor<Sources.DestinationFromNestedSrc>(),
        SourceReader.GetSourceFor<Sources.FlatteningProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MapWithSourceObjectDeepFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.DeepNestedSource>(),
        SourceReader.GetSourceFor<Sources.SourceObjWithDeepNesting>(),
        SourceReader.GetSourceFor<Sources.DestinationFromDeepNestedSrc>(),
        SourceReader.GetSourceFor<Sources.DeepFlatteningProfile>()
    }, _snapshotLocation);

    #endregion
}
