using AutomapGenerator.Benchmarks.Models;
using BenchmarkDotNet.Attributes;

namespace AutomapGenerator.Benchmarks.Automapper;

[MemoryDiagnoser]
public class FlatMapBenchmark {
    private SrcWithNested _simpleSource = null!;
    private AutoMapper.IMapper _autoMapper = null!;
    private Mapper _generatedMapper = null!;

    [GlobalSetup]
    public void Setup() {
        _simpleSource = new() { 
            Child = new()
        };
        _generatedMapper = new Mapper();
        var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<NestedMapProfile>());
        _autoMapper = config.CreateMapper();
    }

    [Benchmark]
    public FlattenedDest Automapper_FlatMap() => _autoMapper.Map<FlattenedDest>(_simpleSource);

    [Benchmark]
    public FlattenedDest AutomapGenerator_FlatMap() => MapProfileRunner.RunFlatteningMap(_simpleSource, _generatedMapper);
}
