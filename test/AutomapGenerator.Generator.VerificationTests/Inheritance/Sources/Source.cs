using System;
namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class Source : ISourceFile {
    public int Id { get; set; }
    public int CreateUserID { get; set; }
    public DateTime CreateDate { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
