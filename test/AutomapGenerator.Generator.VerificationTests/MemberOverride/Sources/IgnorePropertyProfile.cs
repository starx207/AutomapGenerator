namespace AutomapGenerator.Generator.VerificationTests.MemberOverride.Sources;

public class IgnorePropertyProfile : MapProfile, ISourceFile {
    public IgnorePropertyProfile() 
        => CreateMap<SimpleSourceObj, SimpleDestinationObj>().ForMember(d => d.Type, opt => opt.Ignore());

    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
