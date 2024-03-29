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
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithNesting s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc());
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithNesting s, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc d):
                    return (dynamic)MapInternal(s, d);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        private AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc MapInternal(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithNesting source, AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc destination)
        {
            destination.Id = source.Id;
            destination.ChildObjDescription = source.ChildObj?.Description;
            destination.ChildObjOtherProp = source.ChildObj?.OtherProp;
            destination.SourceObjWithNestingChildObjDescription = source.ChildObj?.Description;
            destination.ChildObjNestedSourceOtherProp = source.ChildObj?.OtherProp;
            destination.NestedSourceDescription = source.NestedSource?.Description;

            return destination;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithNesting> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.SourceObjWithNesting> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.CreateMap.Sources.DestinationFromNestedSrc()
                        {
                            Id = source.Id,
                            ChildObjDescription = source.ChildObj != null ? source.ChildObj.Description : null,
                            ChildObjOtherProp = source.ChildObj != null ? source.ChildObj.OtherProp : null,
                            SourceObjWithNestingChildObjDescription = source.ChildObj != null ? source.ChildObj.Description : null,
                            ChildObjNestedSourceOtherProp = source.ChildObj != null ? source.ChildObj.OtherProp : null,
                            NestedSourceDescription = source.NestedSource != null ? source.NestedSource.Description : null
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
