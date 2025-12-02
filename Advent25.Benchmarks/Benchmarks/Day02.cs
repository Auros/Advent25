using BenchmarkDotNet.Attributes;

namespace Advent25.Benchmarks.Benchmarks;

public class Day02
{
    [GlobalSetup]
    public void Setup() => AdventBenchmarks.Setup();
    
    [Benchmark(Baseline = true)]
    public (long, long) Auros()
    {
        return Advent25.Day02.Run(AdventBenchmarks.Input);
    }
}