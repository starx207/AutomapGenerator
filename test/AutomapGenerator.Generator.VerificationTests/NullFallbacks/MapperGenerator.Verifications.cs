using System.IO;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.NullFallbacks;

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
        SourceReader.GetSourceFor<Sources.DestinationObjFromNested>(),
        SourceReader.GetSourceFor<Sources.ProfileWithFallbackForNested>()
    }, _snapshotLocation);

    [Fact]
    public Task MappingNullVsNonNull_RefAndValueTypes() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.DestinationForNullableValueTypes>(),
        SourceReader.GetSourceFor<Sources.SourceWithNullableValueTypes>(),
        SourceReader.GetSourceFor<Sources.ProfileWithNullableValueTypes>()
    }, _snapshotLocation);

    #endregion
}
