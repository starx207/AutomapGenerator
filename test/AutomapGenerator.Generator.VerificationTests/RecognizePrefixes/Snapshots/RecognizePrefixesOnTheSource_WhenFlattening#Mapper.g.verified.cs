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
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithNestedObject s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.FlattenedDestination d):
                    d.ChildDescription = s.TestChild?.TestDescription;
                    d.ChildOtherProp = s.TestChild?.OtherProp;
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
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithNestedObject> s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.FlattenedDestination):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.FlattenedDestination()
                    {ChildDescription = src.TestChild != null ? src.TestChild.TestDescription : null, ChildOtherProp = src.TestChild != null ? src.TestChild.OtherProp : null}));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
