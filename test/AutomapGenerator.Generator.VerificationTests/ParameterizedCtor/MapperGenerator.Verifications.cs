using System.IO;
using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor;
public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("ParameterizedCtor", "Snapshots");

    #region Tests

    [Fact]
    public Task ObjectWithIncompatibleConstructor() => Verifier.Verify(new[] {
       SourceReader.GetSourceFor<Sources.SourceObj>(),
       SourceReader.GetSourceFor(new Sources.DestWithIncompatibleCtor([])),
       SourceReader.GetSourceFor<Sources.ProfileForIncompatibleCtor>()
    }, _snapshotLocation);

    [Fact]
    public Task ObjectWithCompatibleConstructor() => Verifier.Verify(new[] {
       SourceReader.GetSourceFor<Sources.SourceObj>(),
       SourceReader.GetSourceFor(new Sources.DestWithCompatibleCtor(string.Empty)),
       SourceReader.GetSourceFor<Sources.ProfileForCompatibleCtor>()
    }, _snapshotLocation);

    [Fact]
    public Task ObjectWithMultipleConstructors() => Verifier.Verify(new[] {
       SourceReader.GetSourceFor<Sources.SourceObj>(),
       SourceReader.GetSourceFor<Sources.DestWithMultipleCtor>(),
       SourceReader.GetSourceFor<Sources.ProfileForMultipleCtor>()
    }, _snapshotLocation);

    #endregion
}
