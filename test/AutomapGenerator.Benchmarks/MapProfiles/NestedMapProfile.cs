using AutomapGenerator.Benchmarks.Models;

namespace AutomapGenerator.Benchmarks.MapProfiles;
public class NestedMapProfile : MapProfile {
    public NestedMapProfile() => CreateMap<SrcWithNested, FlattenedDest>();
}
