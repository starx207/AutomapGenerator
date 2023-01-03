using AutoMapper;
using AutomapGenerator.Benchmarks.Models;

namespace AutomapGenerator.Benchmarks.Automapper;
public class SimpleMapProfile : Profile {
    public SimpleMapProfile() => CreateMap<SimpleSource, SimpleDestination>();
}
