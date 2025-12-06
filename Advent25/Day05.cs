using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Advent25.Core;

namespace Advent25;

public class Day05 : IAdventSolution
{
    private readonly record struct Pair(long A, long B);
    
    public static (long, long) Run(AdventInput input)
    {
        int part1Count = 0;
        long part2Count = 0;
        
        var data = input.Text.AsSpan();
        var stew = data.IndexOf("\n\n");

        var rangeSpan = data[..stew];
        var pairs = rangeSpan.Count('\n') + 1;
        Span<Pair> loaded = stackalloc Pair[pairs];
        Span<Pair> sorted = stackalloc Pair[pairs];
        int added = 0;
        int validPairCount = 0;
        
        foreach (var pairStr in rangeSpan.Split('\n'))
        {
            var pairSpan = rangeSpan[pairStr];
            var pairSet = pairSpan.Split('-');

            pairSet.MoveNext();
            var a = long.Parse(pairSpan[pairSet.Current], NumberStyles.None);
            pairSet.MoveNext();
            var b = long.Parse(pairSpan[pairSet.Current], NumberStyles.None);

            loaded[added++] = new Pair(a, b);
        }
        
        loaded.Sort(static (a, b) => a.A.CompareTo(b.A));

        foreach (var pair in loaded)
        {
            var (a, b) = pair;
            if (validPairCount is 0)
            {
                // Add the first pair count
                sorted[0] = new Pair(a, b);
                validPairCount++;
                continue;
            }
            
            for (int i = 0; i < validPairCount; i++)
            {
                var existing = sorted[i];
                // First we check if this next pair is entirely within this range
                if (a <= existing.A && b >= existing.B)
                    sorted[i] = new Pair(a, b);
                else if (a <= existing.A && b <= existing.B) // Widen by A
                    sorted[i] = existing with { A = a };
                else if (a <= existing.B && a >= existing.A && b >= existing.B) // Widen by B
                    sorted[i] = existing with { B = b };
                else if (a > existing.A && b > existing.B && i == validPairCount - 1) // Create new entry
                {
                    sorted[validPairCount++] = new Pair(a, b);
                    break;
                }
            }
        }
        
        var dataStartIndex = stew + 2;
        var ids = data[dataStartIndex..];
        
        int idIndex = 0;
        int pairIndex = 0;
        var idCount = ids.Count('\n');
        Span<long> idSpan = stackalloc long[idCount + 1];
        ref Pair sortRef = ref MemoryMarshal.GetReference(sorted);
        
        // 44.62 microseconds
        foreach (var id in ids.Split('\n'))
        {
            var value = long.Parse(ids[id], NumberStyles.None);
            idSpan[idIndex++] = value;
        }
        
        idSpan.Sort(static (a, b) => a.CompareTo(b));
        
        for (int i = 0; i < validPairCount; i++)
        {
            var pair = Unsafe.Add(ref sortRef, i);
            part2Count += pair.B - pair.A + 1;
        }

        foreach (var value in idSpan)
        {
            for (int i = pairIndex; i < validPairCount; i++)
            {
                var pair = Unsafe.Add(ref sortRef, i);
                
                // We skipped its range, so its invalid
                if (value < pair.A)
                    break;

                if (value < pair.A || value > pair.B)
                    continue;

                part1Count++;
                pairIndex = i;
                break;
            }
        }
        return (part1Count, part2Count);
    }
}