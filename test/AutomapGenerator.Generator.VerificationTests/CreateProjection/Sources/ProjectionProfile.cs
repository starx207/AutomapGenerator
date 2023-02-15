namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;

public class ProjectionProfile : MapProfile, ISourceFile {
    public ProjectionProfile() => CreateProjection<ProjectionSource, ProjectionDestination>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
