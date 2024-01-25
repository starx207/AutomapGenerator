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
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithDeepNesting s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromDeepNestedSrc d):
                    d.Level1Level2Description = s.Level1?.Level2?.Description;
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
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithDeepNesting> s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromDeepNestedSrc):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromDeepNestedSrc()
                    {Level1Level2Description = src.Level1 != null && src.Level1.Level2 != null ? src.Level1.Level2.Description : null}));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
