namespace AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources;
public class DestWithIncompatibleCtor : ISourceFile {
    public DestWithIncompatibleCtor(byte[] paramNotInSource) {
        
    }

    public int Id { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
