using AutoMapper;
using AutomapGenerator.Benchmarks.Models;

namespace AutomapGenerator.Benchmarks.Automapper;
public class NestedMapProfile : Profile {
    public NestedMapProfile()
        => CreateMap<SrcWithNested, FlattenedDest>();
}
