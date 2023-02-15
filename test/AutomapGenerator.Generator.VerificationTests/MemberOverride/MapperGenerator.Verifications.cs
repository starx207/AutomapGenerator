using AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

namespace AutomapGenerator.Generator.VerificationTests.MemberOverride;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"MemberOverride\Snapshots";

    #region Tests

    [Fact]
    public Task IgnoreUnmappableProperty() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<SimpleSourceObj>(),
        SourceReader.GetSourceFor<SimpleDestinationObj>(),
        SourceReader.GetSourceFor<IgnorePropertyProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleIgnoredProperties() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<FullDestinationObj>(),
        SourceReader.GetSourceFor<IgnoreMultiplePropsProfile>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task ExplicitMemberMapping() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<FullSourceObj>(),
        SourceReader.GetSourceFor<DestinationThatBreaksNamingConvention>(),
        SourceReader.GetSourceFor<MapExplicitlyProfile>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
