namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class PrefixedDestinationProfile : MapProfile, ISourceFile {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructors", Justification = "<Pending>")]
    public PrefixedDestinationProfile() {
        CreateMap<FullSourceObj, DestinationWithSrcPrefix>();
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
