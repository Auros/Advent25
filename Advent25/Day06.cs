using System.Globalization;
using Advent25.Core;

namespace Advent25;

public class Day06 : IAdventSolution
{
    private readonly record struct Datum(int Value, int Size, Range Range);
    
    public static (long, long) Run(AdventInput input)
    {
        // I JUST finished writing this and only now realized I could parse the input as a matrix
        // oh well, enjoy this cursed solution. I need to read the problem and input more carefully.
        
        long part1Count = 0;
        long part2Count = 0;
        
        var lastLine = input.Lines[^1].AsSpan();
        var problems = lastLine.CountAny('+', '*');
        var lineSize = input.Lines[0].Length;

        var dataRowCount = input.Lines.Length - 1;
        Span<Datum> data = stackalloc Datum[problems * dataRowCount];
        Span<char> numberBuilder = stackalloc char[dataRowCount];
        Span<char> numberWriter = stackalloc char[dataRowCount];

        int added = 0;
        var inputSpan = input.Text.AsSpan();
        var entries = inputSpan.SplitAny(' ', '\n');
        int calculationIndex = -1;

        foreach (var entry in entries)
        {
            // Skip empty entry
            if (entry.Start.Value == entry.End.Value)
                continue;
            
            var value = inputSpan[entry].Trim();
            
            // Skip trimmed entry
            if (value.Length is 0)
                continue;

            if (calculationIndex >= 0 || (value.Length is 1 && value.ContainsAny('+', '*')))
            {
                // calculate final score
                calculationIndex++;
                var multiply = value[0] is '*';

                long p1Result = multiply ? 1 : 0;
                long p2Result = multiply ? 1 : 0;

                Datum highest = default;
                for (int i = 0; i < dataRowCount; i++)
                {
                    var targetIndex = calculationIndex + problems * i;
                    var target = data[targetIndex];
                    if (multiply)
                    {
                        p1Result *= target.Value;
                    }
                    else
                    {
                        p1Result += target.Value;
                    }
                    
                    if (target.Value > highest.Value)
                        highest = target;
                }

                var physicalColumnStartIndex = (highest.Range.End.Value - 1) % (lineSize + 1);
                var outOfBounds = physicalColumnStartIndex - highest.Size;
                for (int i = physicalColumnStartIndex; i > outOfBounds; i--)
                {
                    int digitsAdded = 0;
                    for (int q = 0; q < dataRowCount; q++)
                    {
                        var index = lineSize * q + i + 1 * q;
                        numberBuilder[q] = inputSpan[index];
                    }
                    
                    foreach (var num in numberBuilder)
                        if (num != ' ')
                            numberWriter[digitsAdded++] = num;

                    var rtlNum = int.Parse(numberWriter[..digitsAdded], NumberStyles.None);
                    if (multiply)
                    {
                        p2Result *= rtlNum;
                    }
                    else
                    {
                        p2Result += rtlNum;
                    }
                }
                
                part1Count += p1Result;
                part2Count += p2Result;
            }
            else
            {
                // add to data
                var num = int.Parse(value, NumberStyles.None);
                data[added++] = new Datum(num, value.Length, entry);
            }
        }

        return (part1Count, part2Count);
    }
}