using Advent25.Core;

namespace Advent25;

public class Day03 : IAdventSolution
{
    public static (long, long) Run(AdventInput input)
    {
        int part1 = 0;
        long part2 = 0;

        const int size = 12;
        Span<char> data = stackalloc char[size];
        
        foreach (var bankStr in input.Lines)
        {
            var bank = bankStr.AsSpan();
            // Part 1
            {
                int indexOfHighest = 0;
                char highest = '0';
                char second = '0';

                for (int i = 0; i < bank.Length; i++)
                {
                    var current = bank[i];
                    if (highest >= current || i == bank.Length - 1)
                    {
                        if (i != indexOfHighest && current > second)
                        {
                            second = current;
                        }
                        continue;
                    }

                    indexOfHighest = i;
                    highest = current;
                    second = '0';
                }

                int voltage = (highest - '0') * 10 + second - '0';
                part1 += voltage;
            }
            
            // Part 2
            {
                int cursor = 0;
                int progress = size;
                while (progress > 0)
                {
                    var len = bank.Length;
                    var end = len - progress + 1;
                    if (end > len)
                        end = len;
                    
                    char highest = '0';
                    for (int i = cursor; i < end; i++)
                    {
                        var current = bank[i];
                        if (highest >= current)
                            continue;
                        
                        highest = current;
                        cursor = i + 1;
                    }

                    data[size - progress] = highest;
                    progress--;
                }
                
                var voltage = long.Parse(data);
                part2 += voltage;
            }

        }

        return (part1, part2);
    }
}