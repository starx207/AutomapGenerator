namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

public class IgnoreMultiplePropsProfile : MapProfile, ISourceFile {
    public IgnoreMultiplePropsProfile() 
        => CreateMap<FullSourceObj, FullDestinationObj>()
        .ForMember(d => d.Type, opt => opt.Ignore())
        .ForMember(d => d.InUse, opt => opt.Ignore());

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
