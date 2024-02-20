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
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes d):
                    MapInternal(s, d);
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }

        private void MapInternal(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject source, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes destination)
        {
            destination.DtoId = source.Id;
            destination.OtherType = source.Type;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
            where TDestination : new()
        {
            var destInstance = new TDestination();
            switch (source, destInstance)
            {
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject> s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes d):
                    return global::System.Linq.Queryable.Cast<TDestination>(ProjectInternal(s, d));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes> ProjectInternal(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject> sourceQueryable, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes _)
        {
            return global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithMultiplePrefixes()
            {
                DtoId = source.Id,
                OtherType = source.Type
            });
        }
    }
}
