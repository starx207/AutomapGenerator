namespace AutomapGenerator
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object source)
            where TDestination : new();
        TDestination Map<TDestination>(object source, TDestination destination);
    }
}