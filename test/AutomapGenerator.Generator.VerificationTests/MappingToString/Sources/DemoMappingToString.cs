namespace AutomapGenerator.Generator.VerificationTests.MappingToString.Sources;
public class DemoMappingToString : ISourceFile {
    private readonly IMapper _mapper;

    public DemoMappingToString() : this(null!) {
    }
    public DemoMappingToString(IMapper mapper) => _mapper = mapper;

    public void Test() {
        var obj1 = new ObjWithStringOverride();
        var obj2 = new ObjWithoutStringOverride();

        var test1 = _mapper.Map<string>(obj1);
        var test2 = _mapper.Map<string>(obj2);
        var test3 = _mapper.Map<string>(System.DateTime.Now); // Something about DateTime.Now breaks the test if I don't fully qualify it. Not a problem in the sample projects though
        var test4 = _mapper.Map<string>(31.4);
        var test5 = _mapper.Map<string>(false);
    }

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
