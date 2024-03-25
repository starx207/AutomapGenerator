using System.Linq;

namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping.Sources;
public class ClassThatUsesProjection : ISourceFile {
    private readonly IMapper _mapper;

    public ClassThatUsesProjection() : this(null!) {
    }
    public ClassThatUsesProjection(IMapper mapper) => _mapper = mapper;

    public void DoSomeWork(IQueryable<SourceObj> src) {
        var dest = _mapper.ProjectTo<DestinationObj>(src);
    }
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
