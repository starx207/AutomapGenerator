using AutoMapper;
using AutomapGenerator.Benchmarks.Models;
using BenchmarkDotNet.Attributes;

namespace AutomapGenerator.Benchmarks.Automapper;

[MemoryDiagnoser]
public class CreateAndMapBenchmark {
    private SimpleSource _simpleSource = null!;

    [GlobalSetup]
    public void Setup() => _simpleSource = new();

    [Benchmark]
    public SimpleDestination Automapper_SimpleMap() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SimpleMapProfile>());
        var mapper = config.CreateMapper();
        return mapper.Map<SimpleDestination>(_simpleSource);
    }

    [Benchmark]
    public SimpleDestination AutomapGenerator_SimpleMap() => MapProfileRunner.RunSimpleMap(_simpleSource);
}
