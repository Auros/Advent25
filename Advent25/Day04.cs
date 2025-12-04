using System.Runtime.CompilerServices;
using Advent25.Core;

namespace Advent25;

public class Day04 : IAdventSolution
{
    public static (long, long) Run(AdventInput input)
    {
        var rowSize = input.Lines[0].Length;
        var colSize = input.Lines.Length;
        var data = input.Lines;

        int part1 = 0;
        int part2 = 0;
        var lastColumnIndex = (colSize - 1) * rowSize;
        bool searching = true;

        bool firstAttempt = true;
        Span<char> db = stackalloc char[rowSize * colSize];

        while (searching)
        {
            bool movedAny = false;
            for (int a = 0; a < input.Lines.Length; a++)
            {
                var col = input.Lines[a].AsSpan();
                for (int b = 0; b < col.Length; b++)
                {
                    var i = a * rowSize + b;
                    char current;
                    if (firstAttempt)
                    {
                        current = data[a][b];
                        db[i] = current;
                    }
                    else
                    {
                        current = db[i];
                    }
                    if (current != '@')
                        continue;
                    
                    db[i] = current;
            
                    int count = 0;

                    var topValid = i >= rowSize;
                    var leftValid = i % rowSize != 0;
                    var bottomValid = lastColumnIndex > i;
                    var rightValid = (i + 1) % rowSize != 0;
                
                    if (topValid)
                    {
                        var top = MoveUp(i, rowSize);
                        if (IsThing(top, data, db, firstAttempt, rowSize, colSize))
                            count++;

                        if (leftValid && IsThing(MoveLeft(top), data, db, firstAttempt, rowSize, colSize))
                            count++;
                    
                        if (rightValid && IsThing(MoveRight(top), data, db, firstAttempt, rowSize, colSize))
                            count++;
                    }

                    if (leftValid && IsThing(i - 1, data, db, firstAttempt, rowSize, colSize))
                        count++;
                
                    if (rightValid && IsThing(i + 1, data, db, firstAttempt, rowSize, colSize))
                        count++;
                
                    if (bottomValid)
                    {
                        var bottom = MoveDown(i, rowSize);
                        if (IsThing(bottom, data, db, firstAttempt, rowSize, colSize))
                            count++;

                        if (leftValid && IsThing(MoveLeft(bottom), data, db, firstAttempt, rowSize, colSize))
                            count++;
                    
                        if (rightValid && IsThing(MoveRight(bottom), data, db, firstAttempt, rowSize, colSize))
                            count++;
                    }

                    if (count > 3)
                        continue;
                    if (firstAttempt)
                        part1++;
                        
                    db[i] = '.';
                    movedAny = true;
                    part2++;
                }
            }

            firstAttempt = false;
            searching = movedAny;
        }
        return (part1, part2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int MoveLeft(int index)
    {
        return index - 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int MoveRight(int index)
    {
        return index + 1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int MoveUp(int index, int rowSize)
    {
        return index - rowSize;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int MoveDown(int index, int rowSize)
    {
        return index + rowSize;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsThing(int id, string[] data, Span<char> db, bool firstAttempt, int rowSize, int colSize)
    {
        var x = id % rowSize;
        var y = id / colSize;
        return (firstAttempt ? data[y][x] : db[id]) == '@';
    }
}