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
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp());
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp d):
                    return (dynamic)MapInternal(s, d);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        private AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp MapInternal(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.FullSourceObj source, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp destination)
        {
            destination.Id = source.Id;
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
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationWithReadonlyProp()
                        {
                            Id = source.Id,
                            Timestamp = source.Timestamp,
                            InUse = source.InUse
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
