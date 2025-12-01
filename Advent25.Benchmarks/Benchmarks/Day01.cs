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
    public string Test()
    {
        return Advent25.Day01.Run(AdventBenchmarks.Input);
    }
    
    [Benchmark]
    public string Test2()
    {
        return Advent25.Day01.Run(AdventBenchmarks.Input);
    }
}