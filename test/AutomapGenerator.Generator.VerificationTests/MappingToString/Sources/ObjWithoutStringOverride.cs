namespace AutomapGenerator.Generator.VerificationTests.MappingToString.Sources;
public class ObjWithoutStringOverride : ISourceFile {
    public bool BoolProp { get; set; }
    public string? StringProp { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
