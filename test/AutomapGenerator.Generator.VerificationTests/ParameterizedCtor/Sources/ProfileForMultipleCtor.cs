namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class ProfileForMultipleCtor : MapProfile, ISourceFile {
    public ProfileForMultipleCtor()
        => CreateMap<SourceObj, DestWithMultipleCtor>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
