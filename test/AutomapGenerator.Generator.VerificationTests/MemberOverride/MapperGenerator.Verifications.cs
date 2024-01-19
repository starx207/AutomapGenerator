namespace AutomapGenerator.Generator.VerificationTests.MemberOverride;

[UsesVerify]
public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("MemberOverride", "Snapshots");

    #region Tests

    [Fact]
    public Task IgnoreUnmappableProperty() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SimpleSourceObj>(),
        SourceReader.GetSourceFor<Sources.SimpleDestinationObj>(),
        SourceReader.GetSourceFor<Sources.IgnorePropertyProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task MultipleIgnoredProperties() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.FullDestinationObj>(),
        SourceReader.GetSourceFor<Sources.IgnoreMultiplePropsProfile>()
    }, _snapshotLocation);

    [Fact]
    public Task ExplicitMemberMapping() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.FullSourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationThatBreaksNamingConvention>(),
        SourceReader.GetSourceFor<Sources.MapExplicitlyProfile>()
    }, _snapshotLocation);

    #endregion
}
