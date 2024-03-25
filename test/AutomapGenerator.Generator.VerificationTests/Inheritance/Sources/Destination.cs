using System;
namespace AutomapGenerator.Generator.VerificationTests.Inheritance.Sources;
public class Destination : ISourceFile {
    public int Id { get; set; }
    public int CreateUserID { get; set; }
    public DateTime CreateDate { get; set; }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
