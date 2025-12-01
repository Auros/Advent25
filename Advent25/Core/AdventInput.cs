using System.Text;

namespace Advent25.Core;

public record AdventInput(string Text, string[] Lines, Stream Stream) : IDisposable
{
    public StreamReader StreamReader { get; private set; } = null!;
    
    public void Reset()
    {
        Stream.Seek(0, SeekOrigin.Begin);
        StreamReader = new StreamReader(Stream, Encoding.UTF8, leaveOpen: true);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Stream.Dispose();
    }
}