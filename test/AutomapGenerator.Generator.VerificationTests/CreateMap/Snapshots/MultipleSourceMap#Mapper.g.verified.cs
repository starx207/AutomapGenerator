﻿//HintName: Mapper.g.cs
// <auto-generated/>
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
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleSourceObj s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj d):
                    MapInternal(s, d);
                    break;
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.OtherSimpleSourceObj s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj d):
                    MapInternal(s, d);
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }

        private void MapInternal(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleSourceObj source, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj destination)
        {
            destination.Id = source.Id;
            destination.Type = source.Type;
        }

        private void MapInternal(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.OtherSimpleSourceObj source, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj destination)
        {
            destination.Id = source.Id;
            destination.Type = source.Type;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
            where TDestination : new()
        {
            var destInstance = new TDestination();
            switch (source, destInstance)
            {
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleSourceObj> s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj d):
                    return global::System.Linq.Queryable.Cast<TDestination>(ProjectInternal(s, d));
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.OtherSimpleSourceObj> s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj d):
                    return global::System.Linq.Queryable.Cast<TDestination>(ProjectInternal(s, d));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj> ProjectInternal(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleSourceObj> sourceQueryable, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj _)
        {
            return global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj()
            {
                Id = source.Id,
                Type = source.Type
            });
        }

        private global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj> ProjectInternal(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.OtherSimpleSourceObj> sourceQueryable, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj _)
        {
            return global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SimpleDestinationObj()
            {
                Id = source.Id,
                Type = source.Type
            });
        }
    }
}
