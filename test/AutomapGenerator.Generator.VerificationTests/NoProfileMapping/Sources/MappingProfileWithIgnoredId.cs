namespace AutomapGenerator.Generator.VerificationTests.NoProfileMapping.Sources;
public class MappingProfileWithIgnoredId : MapProfile, ISourceFile {
    public MappingProfileWithIgnoredId() 
        => CreateMap<SourceObj, DestinationObj>()
        .ForMember(d => d.Id, opt => opt.Ignore());
    
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}
