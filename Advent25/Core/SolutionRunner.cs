using System.Text;

namespace Advent25.Core;

public class SolutionRunner(int day)
{
    public async Task<(int, int)> RunAsync<T>() where T : IAdventSolution
    {
        using var input = await LoadInputAsync();
        return T.Run(input);
    }

    public async Task<(int, int)> RunAsync(Type type)
    {
        if (!type.IsAssignableTo(typeof(IAdventSolution)))
        {
            throw new Exception("Type must be a IAdventSolution");
        }
        using var input = await LoadInputAsync();
        var runMethod = type.GetMethod("Run", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        return ((int, int))runMethod!.Invoke(null, [input])!;
    }

    public async Task<AdventInput> LoadInputAsync()
    {
        const string env = "ADVENT25_INPUTS";
        var inputFilePath = Environment.GetEnvironmentVariable(env);
        if (inputFilePath is null)
        {
            throw new Exception($"Please set the \"{env}\" environment variable to the path of the FOLDER that contain the inputs");
        }

        if (!Directory.Exists(inputFilePath))
        {
            throw new DirectoryNotFoundException($"The Advent Inputs directory ({inputFilePath}) could not be found. Please create it!");
        }

        var file = Directory.EnumerateFiles(inputFilePath, $"{day:D2}.txt").FirstOrDefault();

        if (file is null)
        {
            throw new FileNotFoundException($"Unable to find Day {day} input. (Should be {day:D2}.txt)");
        }
        
        var lines = await File.ReadAllLinesAsync(file);
        var text = string.Join(Environment.NewLine, lines);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));

        AdventInput input = new(text, lines, stream);
        input.Reset();
        
        return input;
    }
}