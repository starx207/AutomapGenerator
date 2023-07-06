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
                case (AutomapGenerator.Generator.VerificationTests.Inheritance.Sources.DerivedSource s, AutomapGenerator.Generator.VerificationTests.Inheritance.Sources.Destination d):
                    d.Id = s.Id;
                    d.CreateUserID = s.CreateUserID;
                    d.CreateDate = s.CreateDate;
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
                case (global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.Inheritance.Sources.DerivedSource> s, AutomapGenerator.Generator.VerificationTests.Inheritance.Sources.Destination):
                    return global::System.Linq.Queryable.Cast<TDestination>(global::System.Linq.Queryable.Select(s, src => new AutomapGenerator.Generator.VerificationTests.Inheritance.Sources.Destination()
                    {Id = src.Id, CreateUserID = src.CreateUserID, CreateDate = src.CreateDate}));
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
