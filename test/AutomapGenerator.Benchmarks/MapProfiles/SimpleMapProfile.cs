using AutomapGenerator.Benchmarks.Models;

namespace AutomapGenerator.Benchmarks.MapProfiles;
public class SimpleMapProfile : MapProfile {
    public SimpleMapProfile() => CreateMap<SimpleSource, SimpleDestination>();
}
