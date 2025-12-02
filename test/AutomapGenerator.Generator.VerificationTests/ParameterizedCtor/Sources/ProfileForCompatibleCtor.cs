namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class ProfileForCompatibleCtor : MapProfile, ISourceFile {
    public ProfileForCompatibleCtor()
        => CreateMap<SourceObj, DestWithCompatibleCtor>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
