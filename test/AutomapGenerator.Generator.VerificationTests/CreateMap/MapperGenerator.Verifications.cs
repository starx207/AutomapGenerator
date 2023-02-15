using AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"CreateMap\Snapshots";

    #region Tests

    [Fact]
    public Task SimpleObjectMap() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.BASIC_CREATE_MAP
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleDestinationMap() => Verifier.Verify(new[] {
        SourceObj.SIMPLE_OBJ,
        DestinationObj.SimpleObjWithName("Destination1Obj"),
        DestinationObj.SimpleObjWithName("Destination2Obj"),
        Profiles.SourceToMultipleDestinations("Destination1Obj", "Destination2Obj")
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleSourceMap() => Verifier.Verify(new[] {
        SourceObj.SimpleObjWithName("Source1Obj"),
        SourceObj.SimpleObjWithName("Source2Obj"),
        DestinationObj.SIMPLE_OBJ,
        Profiles.MultipleSourcesToDestination("Source1Obj", "Source2Obj")
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapperWithEmptyConstructor() => Verifier.Verify(Profiles.EMPTY_CONSTRUCTOR, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapperWithNoConstructor() => Verifier.Verify(Profiles.NO_CONSTRUCTOR, SNAPSHOT_LOCATION);

    [Fact]
    public Task SimpleObjectMap_WithArrowFunction() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.ARROW_CONSTRUCTOR
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlyDestinations() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ_WITH_READONLY_PROP,
        Profiles.BASIC_CREATE_MAP
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithReadonlySources() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ_WITH_READONLY_PROP,
        DestinationObj.FULL_OBJ,
        Profiles.BASIC_CREATE_MAP
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectPrefixes() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ_WITH_SRC_PREFIX,
        Profiles.BASIC_CREATE_MAP
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MapWithSourceObjectFlattening() => Verifier.Verify(new[] {
        NestedSrcObj.FULL_OBJ,
        SourceObj.SIMPLE_OBJ_WITH_NESTING,
        DestinationObj.SIMPLE_OBJ_FROM_NESTED,
        Profiles.BASIC_CREATE_MAP
    }, SNAPSHOT_LOCATION);

    #endregion
}
