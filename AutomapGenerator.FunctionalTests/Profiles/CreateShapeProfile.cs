//using AutomapGenerator.FunctionalTests.Models;

//namespace AutomapGenerator.FunctionalTests.Profiles;
//public class CreateShapeProfile : MapProfile {
//    public CreateShapeProfile() {
//        // base shape + audit entity have so many properties that need ignored, this mapping is less verbose
//        CreateMap<CreateShapeCommand, BaseShape>()
//            .ForMember(dest => dest.Id, opts => opts.Ignore())
//            .ForMember(dest => dest.LayerId, opts => opts.Ignore())
//            .ForMember(dest => dest.ShapeColor, opts => opts.MapFrom(source => source.ShapeColor))
//            .ForMember(dest => dest.Altitude, opts => opts.MapFrom(source => source.Altitude))
//            .ForMember(dest => dest.Code, opts => opts.MapFrom(source => source.Code))
//            .ForMember(dest => dest.Description, opts => opts.MapFrom(source => source.Description))
//            .ForMember(dest => dest.Active, opts => opts.Ignore())
//            .ForMember(dest => dest.AdditionalInfo, opts => opts.MapFrom(source => source.AdditionalInfo))
//            .ForMember(dest => dest.ExternalId, opts => opts.MapFrom(source => source.ExternalId))
//            .ForMember(dest => dest.DateLastMoved, opts => opts.Ignore())
//            .ForMember(dest => dest.BuildingNotInCamaData, opts => opts.Ignore())
//            .ForMember(dest => dest.BuildingNotInCamaSketch, opts => opts.Ignore())
//            .ForMember(dest => dest.DateLastAreaChanged, opts => opts.Ignore())
//            .ForMember(dest => dest.Orphaned, opts => opts.Ignore())
//            .ForMember(dest => dest.DeleteShapePending, opts => opts.Ignore())
//            .ForMember(dest => dest.SourceRecId, opts => opts.MapFrom(source => source.SourceRecId))
//            .ForMember(dest => dest.BinaryCheckSum, opts => opts.MapFrom(source => source.BinaryCheckSum))
//            .ForMember(dest => dest.ShapeArea, opts => opts.Ignore())
//            .ForMember(dest => dest.RotationAngle, opts => opts.MapFrom(source => source.RotationAngle))
//            .ForMember(dest => dest.Order, opts => opts.Ignore())
//            .ForMember(dest => dest.CreateDate, opts => opts.Ignore())
//            .ForMember(dest => dest.ChangeDate, opts => opts.Ignore())
//            .ForMember(dest => dest.CreateUser, opts => opts.Ignore())
//            .ForMember(dest => dest.ChangeUser, opts => opts.Ignore())
//            .IncludeAllDerived();

//        CreateMap<CreateShapeCommand, ShapeFootprint>().ForMember((dest) => dest.TbWebLabels, options => options.Ignore());
//        CreateMap<CreateShapeCommand, ShapeOther>().ForMember((dest) => dest.TbWebLabels, options => options.Ignore());

//    }
//}
