namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class ArrowConstructorProfile : MapProfile, ISourceFile {
    public ArrowConstructorProfile() => CreateMap<FullSourceObj, FullDestinationObj>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
