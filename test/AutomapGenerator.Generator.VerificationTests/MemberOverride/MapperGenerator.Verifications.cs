﻿using AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

namespace AutomapGenerator.Generator.VerificationTests.MemberOverride;

[UsesVerify]
public class MapperGenerator_Verifications {
    private const string SNAPSHOT_LOCATION = @"Snapshots\MemberOverride";

    #region Tests

    [Fact]
    public Task IgnoreUnmappableProperty() => Verifier.Verify(new[] {
        SourceObj.SIMPLE_OBJ,
        DestinationObj.SIMPLE_OBJ,
        Profiles.CREATE_MAP_WITH_IGNORE
    }, SNAPSHOT_LOCATION);

    [Fact]
    public Task MultipleIgnoredProperties() => Verifier.Verify(new[] {
        SourceObj.FULL_OBJ,
        DestinationObj.FULL_OBJ,
        Profiles.CREATE_MAP_WITH_MULTIPLE_IGNORES
    }, SNAPSHOT_LOCATION);

    #endregion
}