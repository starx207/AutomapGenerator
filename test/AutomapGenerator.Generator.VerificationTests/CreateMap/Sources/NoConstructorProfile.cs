namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class NoConstructorProfile : MapProfile, ISourceFile {
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
