using BenchmarkDotNet.Attributes;

namespace Advent25.Benchmarks.Benchmarks;

public class Day07
{
    [GlobalSetup]
    public void Setup() => AdventBenchmarks.Setup();
    
    [Benchmark(Baseline = true)]
    public (long, long) Auros()
    {
        return Advent25.Day07.Run(AdventBenchmarks.Input);
    }
}