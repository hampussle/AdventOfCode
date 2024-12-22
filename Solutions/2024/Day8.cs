using Helpers;
using System.Data;
using System.Text.RegularExpressions;

namespace Solutions.Year2024;

public class Day8(int year, int day) : Day(year, day)
{
    public class Antenna(char node)
    {
        public char Node { get; set; } = node;
        public bool HasAntiNode { get; set; }

        public override string ToString()
        {
            if (HasAntiNode)
                return "#";
            return Node.ToString();
        }
    }

    public Grid<Antenna> ParseInput()
    {
        Grid<Antenna> grid = new();
        string[] splitInput = Input.ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                grid.SetCellValue(row, col, new(c));
            }
        }
        return grid;
    }

    public Cell<Antenna>[] GetMatching(Antenna antenna, Grid<Antenna> grid)
    {
        List<Cell<Antenna>> matching = new();

        if (antenna.Node == '.')
            return [.. matching];

        for (int r = 0; r <= grid.RMax; r++)
        {
            for (int c = 0; c <= grid.CMax; c++)
            {
                var candidate = grid.GetCell(r, c);
                if (antenna.Node == candidate?.Value.Node)
                    matching.Add(candidate);
            }
        }
        return [.. matching];
    }

    public Grid<Antenna> PlaceAntiNodes(Cell<Antenna> first, Cell<Antenna> second, Grid<Antenna> grid)
    {
        if (first.Column == second.Column && first.Row == second.Row)
            return grid;

        var cDiff = Math.Abs(first.Column - second.Column);
        var rDiff = Math.Abs(first.Row - second.Row);

        int row = first.Row < second.Row ? first.Row - rDiff :
                  first.Row > second.Row ? first.Row + rDiff :
        first.Row;

        int col = first.Column < second.Column ? first.Column - cDiff :
                  first.Column > second.Column ? first.Column + cDiff :
        first.Column;
        int row2 = first.Row < second.Row ? second.Row + rDiff :
        first.Row > second.Row ? second.Row - rDiff :
        second.Row;
        int col2 = first.Column < second.Column ? second.Column + cDiff :
        first.Column > second.Column ? second.Column - cDiff :
        second.Column;

        if (grid.GetCellValue(row, col) is Antenna antenna1)
        {
            antenna1.HasAntiNode = true;
        }

        if (grid.GetCellValue(row2, col2) is Antenna antenna2)
        {
            antenna2.HasAntiNode = true;
        }

        return grid;
    }

    public Grid<Antenna> PlaceAntiNodes2(Cell<Antenna> first, Cell<Antenna> second, Grid<Antenna> grid)
    {
        if (first.Column == second.Column && first.Row == second.Row)
            return grid;

        var cDiff = Math.Abs(first.Column - second.Column);
        var rDiff = Math.Abs(first.Row - second.Row);

        int multiplier = 1;

        bool bothOutOfBounds = false;

        while (!bothOutOfBounds)
        {
            var cDiff2 = cDiff * multiplier;
            var rDiff2 = rDiff * multiplier;

            int row = first.Row < second.Row ? first.Row - rDiff2 :
            first.Row > second.Row ? first.Row + rDiff2 :
            first.Row;

            int col = first.Column < second.Column ? first.Column - cDiff2 :
                      first.Column > second.Column ? first.Column + cDiff2 :
            first.Column;

            int row2 = first.Row < second.Row ? second.Row + rDiff2 :
            first.Row > second.Row ? second.Row - rDiff2 :
            second.Row;

            int col2 = first.Column < second.Column ? second.Column + cDiff2 :
            first.Column > second.Column ? second.Column - cDiff2 :
            second.Column;

            bothOutOfBounds = true;

            if (grid.GetCellValue(row, col) is Antenna antenna1)
            {
                antenna1.HasAntiNode = true;
                bothOutOfBounds = false;
            }

            if (grid.GetCellValue(row2, col2) is Antenna antenna2)
            {
                antenna2.HasAntiNode = true;
                bothOutOfBounds = false;
            }

            multiplier++;
        }

        return grid;
    }

    public Grid<Antenna> PlaceAntiNodes2(Cell<Antenna> antenna, Cell<Antenna>[] matching, Grid<Antenna> grid)
    {
        List<Cell<Antenna>> all = [.. matching];
        all.Add(antenna);

        for (int i = 0; i < all.Count; i++)
        {
            for (int j = i + 1; j < all.Count; j++)
            {
                grid = PlaceAntiNodes2(all[i], all[j], grid);
            }
        }

        foreach (var cell in all)
        {
            cell.Value.HasAntiNode = true;
        }

        return grid;
    }

    public Grid<Antenna> PlaceAntiNodes(Cell<Antenna> antenna, Cell<Antenna>[] matching, Grid<Antenna> grid)
    {
        List<Cell<Antenna>> all = [.. matching];
        all.Add(antenna);

        for (int i = 0; i < all.Count; i++)
        {
            for (int j = i+1; j < all.Count; j++)
            {
                grid = PlaceAntiNodes(all[i], all[j], grid);
            }
        }

        return grid;
    }

    public override string PartOne()
    {
        var grid = ParseInput();
        grid.PrintGrid();
        HashSet<char> visited = [];
        for (int r = 0; r < grid.RMax; r++)
        {
            for (int c = 0; c < grid.CMax; c++)
            {
                var antenna = grid.GetCell(r, c);

                ArgumentNullException.ThrowIfNull(antenna);

                if (visited.Contains(antenna.Value.Node))
                    continue;

                var matching = GetMatching(antenna.Value, grid);

                if (matching.Length > 0)
                {
                    //Console.WriteLine();
                    //grid.PrintGrid();

                    grid = PlaceAntiNodes(antenna, matching, grid);

                    visited.Add(antenna.Value.Node);

                    //Console.WriteLine();
                    //grid.PrintGrid();
                }
            }
        }
        grid.PrintGrid();
        return grid.grid.Count(a => a.Value.Value.HasAntiNode).ToString();
    }

    public override string PartTwo()
    {
        var grid = ParseInput();
        grid.PrintGrid();
        HashSet<char> visited = [];
        for (int r = 0; r < grid.RMax; r++)
        {
            for (int c = 0; c < grid.CMax; c++)
            {
                var antenna = grid.GetCell(r, c);

                ArgumentNullException.ThrowIfNull(antenna);

                if (visited.Contains(antenna.Value.Node))
                    continue;

                var matching = GetMatching(antenna.Value, grid);

                if (matching.Length > 0)
                {
                    //Console.WriteLine();
                    //grid.PrintGrid();

                    grid = PlaceAntiNodes2(antenna, matching, grid);

                    visited.Add(antenna.Value.Node);

                    //Console.WriteLine();
                    //grid.PrintGrid();
                }
            }
        }
        grid.PrintGrid();
        return grid.grid.Count(a => a.Value.Value.HasAntiNode).ToString();
    }
}