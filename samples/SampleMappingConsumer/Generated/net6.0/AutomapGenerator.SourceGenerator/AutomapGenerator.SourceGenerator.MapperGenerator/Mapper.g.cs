﻿// <auto-generated/>
namespace AutomapGenerator
{
    [global::System.CodeDom.Compiler.GeneratedCode("AutomapGenerator.SourceGenerator.MapperGenerator", "1.0.0.0")]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class Mapper : IMapper
    {
        public TDestination Map<TDestination>(object source)
            where TDestination : new() => Map<TDestination>(source, new TDestination());
        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (SampleMappingConsumer.Models.SourceObj s, SampleMappingConsumer.Models.DestinationObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    d.Timestamp = s.Timestamp;
                    d.InUse = s.InUse;
                    break;
                case (SampleMappingConsumer.Models.DestinationObj s, SampleMappingConsumer.Models.SourceObj d):
                    d.Id = s.Id;
                    d.Type = s.Type;
                    d.Timestamp = s.Timestamp;
                    d.InUse = s.InUse;
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
            where TDestination : new()
        {
            var destInstance = new TDestination();
            switch (source, destInstance)
            {
                case (global::System.Linq.IQueryable<SampleMappingConsumer.Models.SourceObj> s, SampleMappingConsumer.Models.DestinationObj):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new SampleMappingConsumer.Models.DestinationObj() { Id = src.Id, Type = src.Type, Timestamp = src.Timestamp, InUse = src.InUse }));
                case (global::System.Linq.IQueryable<SampleMappingConsumer.Models.DestinationObj> s, SampleMappingConsumer.Models.SourceObj):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new SampleMappingConsumer.Models.SourceObj() { Id = src.Id, Type = src.Type, Timestamp = src.Timestamp, InUse = src.InUse }));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}