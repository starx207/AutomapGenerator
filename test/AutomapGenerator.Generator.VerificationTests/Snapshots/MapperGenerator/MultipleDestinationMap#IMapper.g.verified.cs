﻿//HintName: IMapper.g.cs
// <auto-generated/>
namespace AutomapGenerator
{
    [global::System.CodeDom.Compiler.GeneratedCode("AutomapGenerator.SourceGenerator.MapperGenerator", "1.0.0.0")]
    public interface IMapper
    {
        TDestination Map<TDestination>(object source)
            where TDestination : new();
        TDestination Map<TDestination>(object source, TDestination destination);
        global::System.Linq.IQueryable<TDestination> ProjectTo<TDestination>(global::System.Linq.IQueryable<object> source)
            where TDestination : new();
    }
}
