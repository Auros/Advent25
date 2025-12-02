using System.Globalization;
using System.Runtime.CompilerServices;
using Advent25.Core;

namespace Advent25;

public class Day02 : IAdventSolution
{
    public static (long, long) Run(AdventInput input)
    {
        long part1 = 0;
        long part2 = 0;
        
        Span<int> factors = stackalloc int[3];
        Span<char> buffer = stackalloc char[64];
        Span<long> records = stackalloc long[256];
        int header = 0;

        var inputSpan = input.Text.AsSpan();
        var idPairs = inputSpan.Split(',');
        foreach (Range idPairRange in idPairs)
        {
            // Load each id range dataset without allocating by reusing the initial string
            var idSpan = inputSpan[idPairRange];
            var idSet = idSpan.Split('-');

            // Because .Split on a span returns a split span enumerator,
            // we have to manually enumerate it to pull the values
            idSet.MoveNext();
            var firstIdSpan = idSpan[idSet.Current];
            idSet.MoveNext();
            var lastIdSpan = idSpan[idSet.Current];
            
            var firstId = long.Parse(firstIdSpan, NumberStyles.None);
            var lastId = long.Parse(lastIdSpan, NumberStyles.None);

            var firstDigitCount = GetDigitCount(firstId);
            var lastDigitCount = GetDigitCount(lastId);

            for (int digitCount = firstDigitCount; digitCount <= lastDigitCount; digitCount++)
            {
                var factorCount = GetFactorsLimited(digitCount, ref factors);
                for (int f = 0; f < factorCount; f++)
                {
                    var factor = factors[f];

                    // Huge option to prune here to get speed down, if I am challenged to do so.
                    var minBound = GetPowerForDigits(factor - 1);
                    var maxBound = GetPowerForDigits(factor);

                    // a whole mess
                    for (int i = minBound; i < maxBound; i++)
                    {
                        _ = i.TryFormat(buffer, out int writtenCount);
                        for (int b = writtenCount; b < digitCount; b++)
                        {
                            buffer[b] = buffer[b % writtenCount];
                        }
                        var target = buffer[..digitCount];
                        var invalid = long.Parse(target, NumberStyles.None);

                        if (invalid < firstId || invalid > lastId)
                            continue;
                        
                        if (!records[..header].Contains(invalid))
                        {
                            part2 += invalid;
                            records[header++] = invalid;
                        }
                            
                        if (digitCount / writtenCount == 2)
                            part1 += invalid;
                    }
                }

                header = 0;
                records.Clear();
            }
        }

        return (part1, part2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetDigitCount(long number)
    {
        return number switch
        {
            < 10 => 1,
            < 100 => 2,
            < 1000 => 3,
            < 10000 => 4,
            < 100000 => 5,
            < 1000000 => 6,
            < 10000000 => 7,
            < 100000000 => 8,
            < 1000000000 => 9,
            < 10000000000 => 10,
            < 100000000000 => 11,
            < 1000000000000 => 12,
            < 100000000000000 => 13,
            _ => 14
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetPowerForDigits(int digits)
    {
        return digits switch
        {
            1 => 10,
            2 => 100,
            3 => 1000,
            4 => 10000,
            5 => 100_000,
            6 => 1_000_000,
            7 => 10_000_000,
            8 => 100_000_000,
            9 => 1_000_000_000,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetFactorsLimited(int number, ref Span<int> factors)
    {
        int count = 0;
        switch (number)
        {
            case 2 or 3 or 5 or 7 or 11 or 13:
                factors[count++] = 1;
                break;
            case 4:
                factors[count++] = 2;
                factors[count++] = 1;
                break;
            case 6:
                factors[count++] = 3;
                factors[count++] = 2;
                factors[count++] = 1;
                break;
            case 8:
                factors[count++] = 4;
                factors[count++] = 2;
                factors[count++] = 1;
                break;
            case 9:
                factors[count++] = 3;
                factors[count++] = 1;
                break;
            case 10:
                factors[count++] = 5;
                factors[count++] = 2;
                factors[count++] = 1;
                break;
            case 12:
                factors[count++] = 6;
                factors[count++] = 3;
                factors[count++] = 2;
                factors[count++] = 1;
                break;
        }

        return count;
    }
}