namespace AutomapGenerator;
public abstract class MapProfile {
    protected MapBuilder<TSource, TDestination> CreateMap<TSource, TDestination>() => new();
}
