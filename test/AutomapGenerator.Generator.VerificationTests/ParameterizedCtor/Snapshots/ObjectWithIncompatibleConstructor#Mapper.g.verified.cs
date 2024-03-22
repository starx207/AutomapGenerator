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
                case (AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.SourceObj s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.DestWithIncompatibleCtor):
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} does not have a compatible constructor.");
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.SourceObj s, AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.DestWithIncompatibleCtor d):
                    return (dynamic)MapInternal(s, d);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        private AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.DestWithIncompatibleCtor MapInternal(AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.SourceObj source, AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.DestWithIncompatibleCtor destination)
        {
            destination.Id = source.Id;

            return destination;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.SourceObj> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.SourceObj> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.ParameterizedCtor.Sources.DestWithIncompatibleCtor):
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} does not have a compatible constructor.");
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
