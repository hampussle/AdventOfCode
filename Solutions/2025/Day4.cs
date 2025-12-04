using Helpers;

namespace Solutions.Year2025;

public class Day4(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        Grid<bool> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            var line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                var isPaper = line[col] == '@';
                grid.SetCellValue(row, col, isPaper);
            }
        }

        int total = 0;

        foreach (var cell in grid.grid.Where(c => c.Value.Value))
        {
            var neighbors = grid.GetNeighboringCells(cell.Value.Row, cell.Value.Column);
            if (neighbors.Where(n => n.Value).Count() < 4)
                total++;
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        int total = 0;
        int lastTotal = -1;

        Grid<bool> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            var line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                var isPaper = line[col] == '@';
                grid.SetCellValue(row, col, isPaper);
            }
        }

        while (lastTotal != total)
        {
            lastTotal = total;
            List<(int r, int c)> toRemove = [];
            foreach (var cell in grid.grid.Where(c => c.Value.Value))
            {
                var neighbors = grid.GetNeighboringCells(cell.Value.Row, cell.Value.Column);
                if (neighbors.Where(n => n.Value).Count() < 4)
                {
                    total++;
                    toRemove.Add((cell.Value.Row, cell.Value.Column));
                }
            }

            foreach (var (r, c) in toRemove)
                grid.SetCellValue(r, c, false);
        }

        return total.ToString();
    }

}