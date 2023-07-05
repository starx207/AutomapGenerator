using AutomapGenerator.FunctionalTests.Models;

namespace AutomapGenerator.FunctionalTests.Profiles;
public class DocMetaDataProfile : MapProfile {
    public DocMetaDataProfile() {
        RecognizeDestinationPrefixes("Doc", "New");

        // TODO: The mappings should use null check ternaries. But doing so now causing a build error mapping (string)Type to (DocType)Type.
        //       For now I'll leave out the null checks, but support for those will need to be added.
        CreateMap<DocMetaData, DocSearchViewModel>()
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.DocLen))
            //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type == null ? null : src.Type.Code))
            //.ForMember(dest => dest.SortIndex, opt => opt.MapFrom(src => src.Type == null ? null : src.Type.SortIndex))
            //.ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreateUser == null ? src.DocUser : src.CreateUser.UserName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Code))
            .ForMember(dest => dest.SortIndex, opt => opt.MapFrom(src => src.Type.SortIndex))
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreateUser.UserName));

        CreateMap<DocMetaData, DocumentDownloadViewModel>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.DocContent.DocBinary))
            //.ForMember(dest => dest.FileFormat, opt => opt.MapFrom(src => src.Type == null ? null : src.Type.Code))
            //.ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Type == null ? src.DocTitle : $"{src.DocTitle}.{src.Type.Code.ToLower()}"));
            .ForMember(dest => dest.FileFormat, opt => opt.MapFrom(src => src.Type.Code))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => $"{src.DocTitle}.{src.Type.Code.ToLower()}"));

        // We need this here to take advantage of the destination prefix "New"
        CreateMap<DocMetaData, MoveDocPatch>();
    }
}
