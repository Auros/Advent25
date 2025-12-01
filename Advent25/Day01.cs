using Advent25.Core;

namespace Advent25;

public class Day01 : IAdventSolution
{
    public static (int, int) Run(AdventInput input)
    {
        int dial = 50;
        int part1Count = 0;
        int part2Count = 0;
        foreach (var line in input.Lines)
        {
            var span = line.AsSpan();
            var moveLeft = span[0] == 'L';
            var rotations = int.Parse(span[1..]);
            var gen = rotations / 100;
            int startedAt = dial;
            rotations %= 100;
            rotations *= moveLeft ? -1 : 1;
            dial += rotations;

            int pos = (dial % 100 + 100) % 100;
            if (pos is 0)
            {
                part1Count++;
            }
            
            part2Count += gen;
            if (startedAt is not 0)
            {
                if (dial is > 100 or < 0)
                    part2Count++;
                
                if (pos is 0)
                    part2Count++;
            }

            dial = pos;
        }
        
        return (part1Count, part2Count);
    }
}