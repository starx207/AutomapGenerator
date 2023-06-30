namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping.Sources;
public class ClassThatUsesMapToNew : ISourceFile {
    private readonly IMapper _mapper;

    public ClassThatUsesMapToNew() : this(null!) {
    }
    public ClassThatUsesMapToNew(IMapper mapper) => _mapper = mapper;

    public void DoSomeWork() {
        var src = new SourceObj() {
            Id = 34,
            Description = "My src obj"
        };

        var dest = _mapper.Map<DestinationObj>(src);
    }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
