using AutomapGenerator.FunctionalTests.Models;

namespace AutomapGenerator.FunctionalTests.Profiles;
public class CreateDocumentCommandProfile : MapProfile {
    public CreateDocumentCommandProfile() 
        => CreateMap<NewDocumentInput, CreateDocumentCommand>()
        .ForMember(dest => dest.Content, opt => opt.Ignore());
}
