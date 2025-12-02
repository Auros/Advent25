using Advent25.Core;

namespace Advent25.Benchmarks;

public static class AdventBenchmarks
{
    public static int Day { get; set; }

    public static AdventInput Input { get; set; } = null!;

    public static void Setup()
    {
        int day = DayResolver.ResolveAsync().Result;
        var runner = new SolutionRunner(day);

        Day = day;
        Input = runner.LoadInputAsync().Result;
    }
}