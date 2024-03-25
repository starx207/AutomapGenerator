using System;

namespace AutomapGenerator.Generator.VerificationTests.CreateMap.Sources;

public class SourceObjWithDeepNesting : ISourceFile {
    public Guid Id { get; set; }
    public DeepNestedSource? Level1 { get; set; }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
