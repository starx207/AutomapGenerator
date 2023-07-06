﻿namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class MapWithPrivateSetterDestinationProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public MapWithPrivateSetterDestinationProfile() {
        CreateMap<FullSourceObj, DestinationWithPrivateSetterProp>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
