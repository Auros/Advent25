using Advent25.Benchmarks;
using Advent25.Core;
using BenchmarkDotNet.Running;

int day = await DayResolver.ResolveAsync();

var type = typeof(Program).Assembly
    .GetTypes()
    .FirstOrDefault(t =>
        t.FullName != null
        && t.FullName.StartsWith("Advent25.Benchmarks.Benchmarks", StringComparison.InvariantCulture)
        && t.Name.Contains($"{day:D2}")
    );

if (type is null)
{
    Console.WriteLine($"No benchmark found for Day {day}");
    return;
}

BenchmarkRunner.Run(type, new AdventConfig());