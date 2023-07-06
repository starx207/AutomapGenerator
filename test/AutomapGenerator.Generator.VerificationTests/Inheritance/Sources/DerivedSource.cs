namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class DerivedSource : BaseClass, ISourceFile {
    public int Id { get; set; }
    public new string GetSourceFilePath() => SourceReader.WhereAmI();
}
