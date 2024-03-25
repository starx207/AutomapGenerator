using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AutomapGenerator.Generator.VerificationTests.CreateProjection;

public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("CreateProjection", "Snapshots");

    #region Tests

    [Fact]
    public Task SimpleObjectProjection() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ProjectionSource>(),
        SourceReader.GetSourceFor<Sources.ProjectionDestination>(),
        SourceReader.GetSourceFor<Sources.ProjectionProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task SourceProjectionInMultipleProfiles() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ProjectionSource>(),
        SourceReader.GetSourceFor<Sources.ProjectionDestination>(),
        SourceReader.GetSourceFor<Sources.AlternateDestination>(),
        SourceReader.GetSourceFor<Sources.ProjectionProfile>(),
        SourceReader.GetSourceFor<Sources.AlternateProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task SourceProjectionInProfileAndAdHoc() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.ProjectionSource>(),
        SourceReader.GetSourceFor<Sources.ProjectionDestination>(),
        SourceReader.GetSourceFor<Sources.AlternateDestination>(),
        SourceReader.GetSourceFor<Sources.ProjectionProfile>(),
        SourceReader.GetSourceFor<Sources.ClassUsingAlternateDestMap>()
    }, _snapshotLocation);

    #endregion
}
