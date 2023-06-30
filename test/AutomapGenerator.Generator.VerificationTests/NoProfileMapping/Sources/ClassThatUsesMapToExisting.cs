namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping.Sources;
public class ClassThatUsesMapToExisting : ISourceFile {
    private readonly IMapper _mapper;

    public ClassThatUsesMapToExisting() : this(null!) {
    }
    public ClassThatUsesMapToExisting(IMapper mapper) => _mapper = mapper;

    public void DoSomeWork() {
        var src = new SourceObj() {
            Id = 34,
            Description = "My src obj"
        };

        var dest = new DestinationObj();

        _mapper.Map(src, dest);
    }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
