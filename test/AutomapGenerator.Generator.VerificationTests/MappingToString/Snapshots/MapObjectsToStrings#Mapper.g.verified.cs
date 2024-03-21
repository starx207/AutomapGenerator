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
                case (_, System.Type t) when t == typeof(string):
                    return (dynamic)source?.ToString()!;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (_, string d):
                    return (dynamic)source?.ToString()!;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.MappingToString.Sources.ObjWithStringOverride> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.MappingToString.Sources.ObjWithoutStringOverride> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<System.DateTime> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<double> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<bool> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.MappingToString.Sources.ObjWithStringOverride> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(string):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => source == null ? null : source.ToString()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.MappingToString.Sources.ObjWithoutStringOverride> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(string):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => source == null ? null : source.ToString()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<System.DateTime> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(string):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => source.ToString()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<double> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(string):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => source.ToString()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<bool> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(string):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => source.ToString()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
