namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class ProfileForIncompatibleCtor : MapProfile, ISourceFile {
    public ProfileForIncompatibleCtor()
        => CreateMap<SourceObj, DestWithIncompatibleCtor>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
