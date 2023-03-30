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
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject d):
                    d.Id = s.TestId;
                    d.Type = s.TestType;
                    d.Timestamp = s.TestTimestamp;
                    d.InUse = s.TestInUse;
                    break;
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject d):
                    break;
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix d):
                    d.DtoId = s.Id;
                    d.DtoType = s.Type;
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
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix> s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject()
                    {Id = src.TestId, Type = src.TestType, Timestamp = src.TestTimestamp, InUse = src.TestInUse}));
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix> s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject()
                    {}));
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject> s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix()
                    {DtoId = src.Id, DtoType = src.Type}));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
