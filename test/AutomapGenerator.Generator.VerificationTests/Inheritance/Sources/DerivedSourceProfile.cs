namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class DerivedSourceProfile : MapProfile, ISourceFile {
    public DerivedSourceProfile() => CreateMap<DerivedSource, Destination>();
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
