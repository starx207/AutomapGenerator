namespace AutomapGenerator.Generator.VerificationTests.MappingToString.Sources;
public class ObjWithStringOverride : ISourceFile {
    public int Length { get; set; }
    public string SomeProp { get; set; } = string.Empty;

    public override string ToString() => $"Length: {Length}; SomeProp: \"{SomeProp}\"";
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
