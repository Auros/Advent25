namespace Advent25.Core;

public static class DayResolver
{
    public static async Task<int> ResolveAsync()
    {
        const string env = "ADVENT25_DAY";
        var inputFilePath = Environment.GetEnvironmentVariable(env);
        
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (inputFilePath is null)
        {
            throw new Exception($"Please set the \"{env}\" environment variable to a file that contains the day you want to run");
        }
        
        return int.Parse(await File.ReadAllTextAsync(inputFilePath));
    }
}