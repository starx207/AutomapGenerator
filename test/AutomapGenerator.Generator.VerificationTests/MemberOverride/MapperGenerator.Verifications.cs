namespace AutomapGenerator.Generator.VerificationTests.MemberOverride;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"MemberOverride\Snapshots";

    #region Tests

    [Fact]
    public Task IgnoreUnmappableProperty() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.IgnorePropertyProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleIgnoredProperties() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.IgnoreMultiplePropsProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task ExplicitMemberMapping() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationThatBreaksNamingConvention>(),
        SourceReader.GetSourceFor<Sources.MapExplicitlyProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
