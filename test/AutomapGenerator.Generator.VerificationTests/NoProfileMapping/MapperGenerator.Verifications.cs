namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"NoProfileMapping\Snapshots";

    #region Tests

    [Fact]
    public Task GenerateSimpleMapping_WhenMapToNewInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task GenerateSimpleMapping_WhenMapToExistingInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToExisting>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task GenerateSimpleMapping_WhenProjectToInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesProjection>()
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task GenerateSimpleMapping_WhenMapAllMapperMethodsInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesProjection>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToExisting>(),
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task LetMappingProfileOverrideSimpleMapping_WhenThereIsAProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>(),
        SourceReader.GetSourceFor<Sources.MappingProfileWithIgnoredId>()
    }, SNAPSHOT_LOCATION);

    #endregion
}
