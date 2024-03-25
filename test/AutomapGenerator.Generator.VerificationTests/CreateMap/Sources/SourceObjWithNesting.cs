using System;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class SourceObjWithNesting : ISourceFile {
    public Guid Id { get; set; }
    public NestedSource? ChildObj { get; set; }
    public NestedSource? NestedSource { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
