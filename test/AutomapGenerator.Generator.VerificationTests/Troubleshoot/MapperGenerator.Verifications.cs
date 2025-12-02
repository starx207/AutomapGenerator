using System.Threading.Tasks;

namespace AutomapGenerator.Generator.VerificationTests.Troubleshoot;
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"Troubleshoot\Snapshots";

    #region Tests

    [Fact(Skip = "Troubleshooting only")]
    //[Fact]
    public Task DebugTests() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.TestSource>(),
        SourceReader.GetSourceFor<Sources.TestDestination>(),
        SourceReader.GetSourceFor<Sources.TestProfile>(),
        SourceReader.GetSourceFor<Sources.Demo>()
    }, SNAPSHOT_LOCATION);

    #endregion
}

