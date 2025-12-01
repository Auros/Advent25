using Advent25.Core;
using BenchmarkDotNet.Attributes;

namespace Advent25.Benchmarks.Benchmarks;

public class Day01
{
    [GlobalSetup]
    public void Setup()
    {
        int day = DayResolver.ResolveAsync().Result;
        var runner = new SolutionRunner(day);

        AdventBenchmarks.Day = day;
        AdventBenchmarks.Input = runner.LoadInputAsync().Result;
    }
    
    [Benchmark(Baseline = true)]
    public (int, int) Auros()
    {
        return Advent25.Day01.Run(AdventBenchmarks.Input);
    }
}