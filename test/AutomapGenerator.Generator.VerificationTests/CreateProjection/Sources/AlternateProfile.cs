namespace AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources;

public class AlternateProfile : MapProfile, ISourceFile {
    public AlternateProfile() => CreateProjection<ProjectionSource, AlternateDestination>();

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
