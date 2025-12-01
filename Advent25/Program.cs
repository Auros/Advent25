using Advent25.Core;

int day = await DayResolver.ResolveAsync();
var solution = SolutionFinder.ForDay<Program>("Advent25", day);

SolutionRunner runner = new(day);
var result = await runner.RunAsync(solution);
Console.WriteLine(result);