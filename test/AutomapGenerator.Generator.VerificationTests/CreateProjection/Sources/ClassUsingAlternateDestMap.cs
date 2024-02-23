namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;
public class ClassUsingAlternateDestMap : ISourceFile {
    private readonly IMapper _mapper = null!;

    public IQueryable<AlternateDestination> Test(IQueryable<ProjectionSource> source)
        => _mapper.ProjectTo<AlternateDestination>(source);

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
