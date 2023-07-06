namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class BaseClass : ISourceFile {
    public int CreateUserID { get; set; }
    public DateTime CreateDate { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
