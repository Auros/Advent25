namespace Advent25.Core;

public static class SolutionFinder
{
    public static Type ForDay<TAssemblyType>(string @namespace, int day)
    {
        var type = typeof(TAssemblyType).Assembly
            .GetTypes()
            .FirstOrDefault(t =>
                t.FullName != null
                && t.IsAssignableTo(typeof(IAdventSolution))
                && t.FullName.StartsWith(@namespace, StringComparison.InvariantCulture)
                && t.Name.Contains($"{day:D2}")
            );

        return type ?? throw new Exception($"Cannot find Advent Solution under {@namespace} for Day {day}");
    }
}