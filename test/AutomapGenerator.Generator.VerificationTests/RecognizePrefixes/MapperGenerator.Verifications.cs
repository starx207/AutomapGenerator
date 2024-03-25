using System.IO;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.RecognizePrefixes;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("RecognizePrefixes", "Snapshots");

    #region Tests

    [Fact]
    public Task RecognizePrefixesOnTheSource() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithMultiplePrefixes>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.ProfileForSourceWithMultiplePrefixes>()
    }, _snapshotLocation);

    [Fact]
    public Task RecognizePrefixesOnTheDestination() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.DestinationWithMultiplePrefixes>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.ProfileForDestinationWithMultiplePrefixes>()
    }, _snapshotLocation);

    [Fact]
    public Task KeepRecognizedPrefixesScopedToAProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.OtherSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.UnprefixedObject>(),
        SourceReader.GetSourceFor<Sources.DestinationWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.ProfileForSourceWithSinglePrefix>(),
        SourceReader.GetSourceFor<Sources.ProfileWithoutRecognizedPrefixes>(),
        SourceReader.GetSourceFor<Sources.ProfileForDestinationWithSinglePrefix>()
    }, _snapshotLocation);

    [Fact]
    public Task RecognizePrefixesOnTheSource_WhenFlattening() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.NestedSource>(),
        SourceReader.GetSourceFor<Sources.SourceWithNestedObject>(),
        SourceReader.GetSourceFor<Sources.FlattenedDestination>(),
        SourceReader.GetSourceFor<Sources.PrefixedFlatMapperProfile>()
    }, _snapshotLocation);

    #endregion
}
