using AutomapGenerator.Benchmarks.Models;

namespace AutomapGenerator.Benchmarks;
public static class MapProfileRunner {
    public static SimpleDestination RunSimpleMap(SimpleSource source) {
        var mapper = new Mapper();
        return mapper.Map<SimpleDestination>(source);
    }

    public static SimpleDestination RunSimpleMap(SimpleSource source, Mapper mapper)
        => mapper.Map<SimpleDestination>(source);
}
