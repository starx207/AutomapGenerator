﻿//HintName: Mapper.g.cs
// <auto-generated/>
namespace AutomapGenerator
{
    [global::System.CodeDom.Compiler.GeneratedCode("AutomapGenerator.SourceGenerator.MapperGenerator", "1.0.0.0")]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Mapper : IMapper
    {
        public TDestination Map<TDestination>(object source)
        {
            switch (source, typeof(TDestination))
            {
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix());
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix d):
                    return (dynamic)MapInternal(s, d);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        private AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix MapInternal(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj source, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix destination)
        {
            destination.FullSourceObjId = source.Id;
            destination.FullSourceObjType = source.Type;
            destination.Timestamp = source.Timestamp;
            destination.InUse = source.InUse;

            return destination;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithSrcPrefix()
                        {
                            FullSourceObjId = source.Id,
                            FullSourceObjType = source.Type,
                            Timestamp = source.Timestamp,
                            InUse = source.InUse
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
