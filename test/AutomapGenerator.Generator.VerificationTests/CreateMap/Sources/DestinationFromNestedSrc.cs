namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class DestinationFromNestedSrc : ISourceFile {
    public Guid Id { get; set; }
    public string? ChildObjDescription { get; set; }
    public string? ChildObjOtherProp { get; set; }
    public string? SourceObjWithNestingChildObjDescription { get; set; }
    public string? ChildObjNestedSourceOtherProp { get; set; }
    public string? NestedSourceDescription { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
