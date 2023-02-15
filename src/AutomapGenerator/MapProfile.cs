namespace AutomapGenerator;
public abstract class MapProfile {
    protected MapBuilder<TSource, TDestination> CreateMap<TSource, TDestination>() => new();
    protected MapBuilder<TSource, TDestination> CreateProjection<TSource, TDestination>() => new();
    protected void RecognizePrefixes(params string[] prefixes) { }
}
