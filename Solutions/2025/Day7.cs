using Helpers;

namespace Solutions.Year2025;

public class Day7(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        Grid<char> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string? line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                grid.SetCellValue(row, col, c);
            }
        }

        int total = 0;
        for (int row = 0; row < grid.RMax; row++)
        {
            for (int col = 0; col <= grid.CMax; col++)
            {
                char c = grid.GetCellValue(row, col);
                if (c != '|' && c != 'S')
                    continue;

                char below = grid.GetCellValue(row + 1, col);
                if (below == '.')
                {
                    grid.SetCellValue(row + 1, col, '|');
                }
                else if (below == '^')
                {
                    grid.SetCellValue(row + 1, col + 1, '|');
                    grid.SetCellValue(row + 1, col - 1, '|');
                    total++;
                }
            }
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        Grid<char> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string? line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                grid.SetCellValue(row, col, c);
            }
        }

        Dictionary<int, long> timelines = [];
        for (int row = 0; row < grid.RMax; row++)
        {
            for (int col = 0; col <= grid.CMax; col++)
            {
                char c = grid.GetCellValue(row, col);
                if (c == 'S')
                {
                    timelines[col] = 1;
                    for (int i = 0; i <= grid.CMax; i++)
                    {
                        timelines[i] = 0;
                    }
                    timelines[col] = 1;
                    grid.SetCellValue(row + 1, col, '|');
                    continue;
                }

                if (c != '|')
                    continue;

                char below = grid.GetCellValue(row + 1, col);
                if (below == '.')
                {
                    grid.SetCellValue(row + 1, col, '|');
                }
                else if (below == '^')
                {
                    grid.SetCellValue(row + 1, col + 1, '|');
                    grid.SetCellValue(row + 1, col - 1, '|');
                    timelines[col + 1] += timelines[col];
                    timelines[col - 1] += timelines[col];
                    timelines[col] = 0;
                }
            }
        }

        return timelines.Values.Sum().ToString();
    }

}