namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping;

[UsesVerify]
public class MapperGenerator_Verifications {
    private static readonly string _snapshotLocation = Path.Combine("NoProfileMapping", "Snapshots");

    #region Tests

    [Fact]
    public Task GenerateSimpleMapping_WhenMapToNewInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>()
    }, _snapshotLocation);

    [Fact]
    public Task GenerateSimpleMapping_WhenMapToExistingInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToExisting>()
    }, _snapshotLocation);

    [Fact]
    public Task GenerateSimpleMapping_WhenProjectToInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesProjection>()
    }, _snapshotLocation);

    [Fact]
    public Task GenerateSimpleMapping_WhenMapAllMapperMethodsInvoked_WithNoProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesProjection>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToExisting>(),
    }, _snapshotLocation);

    [Fact]
    public Task LetMappingProfileOverrideSimpleMapping_WhenThereIsAProfile() => Verifier.Verify(new[] {
        SourceReader.GetSourceFor<Sources.SourceObj>(),
        SourceReader.GetSourceFor<Sources.DestinationObj>(),
        SourceReader.GetSourceFor<Sources.ClassThatUsesMapToNew>(),
        SourceReader.GetSourceFor<Sources.MappingProfileWithIgnoredId>()
    }, _snapshotLocation);

    #endregion
}
