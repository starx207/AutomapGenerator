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
                case (AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.SourceObj s, AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj d):
                    MapInternal(s, d);
                    break;
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }

            return destination;
        }

        private void MapInternal(AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.SourceObj source, AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj destination)
        {
            destination.MappedString = source.NullableString ?? source.NonNullString;
            destination.NullableMappedString = source.NullableString ?? source.OtherNullableString;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
            where TDestination : new()
        {
            var destInstance = new TDestination();
            switch (source, destInstance)
            {
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.SourceObj> s, AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj d):
                    return global::System.Linq.Queryable.Cast<TDestination>(ProjectInternal(s, d));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj> ProjectInternal(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.SourceObj> sourceQueryable, AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj _)
        {
            return global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.NullFallbacks.Sources.DestinationObj()
            {
                MappedString = source.NullableString != null ? source.NullableString : source.NonNullString,
                NullableMappedString = source.NullableString != null ? source.NullableString : source.OtherNullableString
            });
        }
    }
}
