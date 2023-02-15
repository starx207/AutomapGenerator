namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class EmptyConstructorProfile : MapProfile, ISourceFile {
    public EmptyConstructorProfile() {

    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
