using BenchmarkDotNet.Attributes;

namespace Advent25.Benchmarks.Benchmarks;

public class Day04
{
    [GlobalSetup]
    public void Setup() => AdventBenchmarks.Setup();
    
    [Benchmark(Baseline = true)]
    public (long, long) Auros()
    {
        return Advent25.Day04.Run(AdventBenchmarks.Input);
    }
}