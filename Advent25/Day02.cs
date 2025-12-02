using System.Globalization;
using System.Runtime.CompilerServices;
using Advent25.Core;

namespace Advent25;

public class Day02 : IAdventSolution
{
    public static (long, long) Run(AdventInput input)
    {
        long part1 = 0;
        
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
            
            // Part 1: If both ids are the same length and odd, it's impossible for an invalid id to appear
            if (firstIdSpan.Length == lastIdSpan.Length && firstIdSpan.Length % 2 != 0)
                continue;
            
            var firstId = long.Parse(firstIdSpan, NumberStyles.None);
            var lastId = long.Parse(lastIdSpan, NumberStyles.None);

            var firstDigitCount = GetDigitCount(firstId);
            var lastDigitCount = GetDigitCount(lastId);

            // this seems dubious, I need to double-check this logic
            var min = (int)(firstId / GetHalvingPowerForDigits(firstDigitCount % 2 == 0 ? firstDigitCount : firstDigitCount + 1));
            var max = (int)(lastId / GetHalvingPowerForDigits(lastDigitCount % 2 == 0 ? lastDigitCount : lastDigitCount - 1));
            
            for (int i = min; i <= max; i++)
            {
                var digitCount = GetDigitCount(i);
                var invalid = DoubleValueVisually(i, digitCount);

                var tooSmall = invalid < firstId;
                var tooLarge = invalid > lastId;
                if (!tooSmall && !tooLarge) // If it's in range we add it to the sum
                    part1 += invalid;
                else if (tooLarge) // If it's too large, we end early
                    break;
            }
        }

        return (part1, 0);
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
    private static int GetHalvingPowerForDigits(long digits)
    {
        return digits switch
        {
            2 => 10,
            4 => 100,
            6 => 1000,
            8 => 10000,
            10 => 100000,
            _ => 0
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

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetPossibleInvalidDigits(int number, ref Span<int> invalidDigits)
    {
        int num = 0;
        int count = 0;
        var digits = GetDigitCount(number);
        
        if (digits == 1)
        {
            invalidDigits[count++] = 11 * number;
        }
        else if (digits == 2)
        {
            invalidDigits[count++] = 101 * number;
        }
        else if (digits == 3)
        {
            invalidDigits[count++] = 10010 * number;
        }
        else if (digits == 4)
        {
            invalidDigits[count++] = 10010 * number;
        }
        
        return count;
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DoubleValueVisually(int input, int digits)
    {
        return input + input * GetPowerForDigits(digits);
    }
}