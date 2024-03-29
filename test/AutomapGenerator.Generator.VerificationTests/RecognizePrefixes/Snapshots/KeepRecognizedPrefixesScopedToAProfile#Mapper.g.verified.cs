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
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject());
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject());
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject s, System.Type t) when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix):
                    return (dynamic)MapInternal(s, new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix());
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        public TDestination Map<TDestination>(object source, TDestination destination)
        {
            switch (source, destination)
            {
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject d):
                    return (dynamic)MapInternal(s, d);
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject d):
                    return (dynamic)MapInternal(s, d);
                case (AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject s, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix d):
                    return (dynamic)MapInternal(s, d);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
            }
        }

        private AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject MapInternal(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix source, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject destination)
        {
            destination.Id = source.TestId;
            destination.Type = source.TestType;
            destination.Timestamp = source.TestTimestamp;
            destination.InUse = source.TestInUse;

            return destination;
        }

        private AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject MapInternal(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix source, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject destination)
        {

            return destination;
        }

        private AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix MapInternal(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject source, AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix destination)
        {
            destination.DtoId = source.Id;
            destination.DtoType = source.Type;

            return destination;
        }

        public global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
        {
            switch (source)
            {
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix> s:
                    return ProjectInternal<TDestination>(s);
                case global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject> s:
                    return ProjectInternal<TDestination>(s);
                default:
                    throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.SourceWithSinglePrefix> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject()
                        {
                            Id = source.TestId,
                            Type = source.TestType,
                            Timestamp = source.TestTimestamp,
                            InUse = source.TestInUse
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.OtherSourceWithSinglePrefix> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject()));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }

        private global::System.Linq.IQueryable<TDestination> ProjectInternal<TDestination>(global::System.Linq.IQueryable<AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.UnprefixedObject> sourceQueryable)
        {
            switch (typeof(TDestination))
            {
                case System.Type t when t == typeof(AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix):
                    return global::System.Linq.Queryable.Cast<TDestination>(
                        global::System.Linq.Queryable.Select(sourceQueryable, source => new AutomapGenerator.Generator.VerificationTests.RecognizePrefixes.Sources.DestinationWithSinglePrefix()
                        {
                            DtoId = source.Id,
                            DtoType = source.Type
                        }));
                default:
                    throw new MappingException($"Mapping from {sourceQueryable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
            }
        }
    }
}
