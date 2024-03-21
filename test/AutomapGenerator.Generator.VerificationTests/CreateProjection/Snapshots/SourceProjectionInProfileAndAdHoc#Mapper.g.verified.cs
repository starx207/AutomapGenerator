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
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.ProjectionSource> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.ProjectionSource> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.ProjectionDestination):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.ProjectionDestination()
                        {
                            Id = source.Id,
                            Type = source.Type,
                            Timestamp = source.Timestamp,
                            InUse = source.InUse
                        }));
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.AlternateDestination):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateProjection.Sources.AlternateDestination()
                        {
                            Id = source.Id,
                            Type = source.Type
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}