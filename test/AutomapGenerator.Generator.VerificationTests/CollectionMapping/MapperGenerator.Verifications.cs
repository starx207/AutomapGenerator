using System.IO;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.CollectionMapping;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("CollectionMapping", "Snapshots");

    #region Tests

    [Fact]
    public Task MapOneCollectionToAnother() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatMapsCollections>()
    }, _snapshotLocation);

    #endregion
}
