namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class DerivedDestinationProfile : MapProfile, ISourceFile {
    public DerivedDestinationProfile() => CreateMap<Source, DerivedDestination>();
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
