namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class AllDerivedProfile : MapProfile, ISourceFile {
    public AllDerivedProfile() => CreateMap<DerivedSource, DerivedDestination>();
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
