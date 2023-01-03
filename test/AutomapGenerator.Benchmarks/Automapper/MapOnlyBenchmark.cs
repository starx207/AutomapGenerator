using AutomapGenerator.Benchmarks.Models;
using BenchmarkDotNet.Attributes;

namespace AutomapGenerator.Benchmarks.Automapper;

[MemoryDiagnoser]
public class MapOnlyBenchmark {
    private SimpleSource _simpleSource = null!;
    private AutoMapper.IMapper _autoMapper = null!;
    private Mapper _generatedMapper = null!;

    [GlobalSetup]
    public void Setup() {
        _simpleSource = new();
        _generatedMapper = new Mapper();
        var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<SimpleMapProfile>());
        _autoMapper = config.CreateMapper();
    }

    [Benchmark]
    public SimpleDestination Automapper_SimpleMap() => _autoMapper.Map<SimpleDestination>(_simpleSource);

    [Benchmark]
    public SimpleDestination AutomapGenerator_SimpleMap() => MapProfileRunner.RunSimpleMap(_simpleSource, _generatedMapper);
}
