using AutomapGenerator.FunctionalTests.Models;

namespace AutomapGenerator.FunctionalTests.Profiles;
public class DocMetaDataProfile : MapProfile {
    public DocMetaDataProfile() {
        RecognizeDestinationPrefixes("Doc", "New");

        CreateMap<DocMetaData, DocSearchViewModel>()
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.DocLen))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type!.Code))
            .ForMember(dest => dest.SortIndex, opt => opt.MapFrom(src => src.Type!.SortIndex))
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreateUser!.UserName, src => src.DocUser));

        CreateMap<DocMetaData, DocumentDownloadViewModel>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.DocContent.DocBinary))
            .ForMember(dest => dest.FileFormat, opt => opt.MapFrom(src => src.Type!.Code, string.Empty))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.DocTitle, string.Empty));

        // We need this here to take advantage of the destination prefix "New"
        CreateMap<DocMetaData, MoveDocPatch>();
    }
}
