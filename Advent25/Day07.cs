using Advent25.Core;

namespace Advent25;

public class Day07 : IAdventSolution
{
    public static (long, long) Run(AdventInput input)
    {
        var firstLine = input.Lines[0];
        var startIndex = firstLine.IndexOf('S');
        Span<long> timelines = stackalloc long[firstLine.Length];
        timelines[startIndex] = 1;
        
        int splitCount = 0;
        long timelineCount = 1;
        
        foreach (var row in input.Lines)
        {
            for (int q = 0; q < timelines.Length; q++)
            {
                var activeBeam = timelines[q] is not 0;
                var splitter = row[q] is '^';

                if (!splitter || !activeBeam)
                    continue;

                var timeline = timelines[q];
                timelines[q - 1] += timeline;
                timelines[q + 1] += timeline;
                timelines[q] = 0;

                splitCount++;
                timelineCount += timeline;
            }
        }
        
        return (splitCount, timelineCount);
    }
}