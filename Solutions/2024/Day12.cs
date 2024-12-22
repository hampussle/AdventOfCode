using Helpers;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Solutions.Year2024;

public class Day12(int year, int day) : Day(year, day)
{
    List<Cell<char>> GetNeighborsWithSameFlower(Cell<char> cell, Grid<char> grid)
    {
        return grid.GetNeighboringCellsExcludeDiagonal(cell.Row, cell.Column).Where(x => x.Value == cell?.Value).ToList();
    }

    List<Cell<char>> GetGardenPlot((int row, int col) pos, Grid<char> grid)
    {
        var cell = grid.GetCell(pos.row, pos.col);
        List<Cell<char>> cells = [];
        Queue<Cell<char>> queue = new();
        queue.Enqueue(cell);
        while (queue.TryDequeue(out var newpos))
        {
            cells.Add(newpos);
            var neighbors = grid.GetNeighboringCellsExcludeDiagonal(newpos.Row, newpos.Column).Where(x => x.Value == cell?.Value && !cells.Any(cell => cell.Row == x.Row && cell.Column == x.Column) && !queue.Any(cell => cell.Row == x.Row && cell.Column == x.Column));
            foreach (var neighbor in neighbors)
            {
                queue.Enqueue(neighbor);
            }
        }
        return cells;
    }

    (int area, List<Cell<char>> visited) GetArea((int row, int col) pos, Grid<char> grid)
    {
        var plot = GetGardenPlot(pos, grid);
        return (plot.Count, plot);
    }

    (int perimeter, List<Cell<char>> visited) GetPerimeter((int row, int col) pos, Grid<char> grid)
    {
        int total = 0;
        var flowers = GetGardenPlot(pos, grid);
        foreach (var flower in flowers)
        {
            var neighbors = GetNeighborsWithSameFlower(flower, grid);
            total += 4 - neighbors.Count;
        }
        return (total, flowers);
    }

    public override string PartOne()
    {
        Grid<char> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                grid.SetCellValue(row, col, c);
            }
        }

        int total = 0;
        HashSet<(int row, int col)> visited = new();
        for (int row = 0; row <= grid.RMax; row++)
        {
            for (int col = 0; col <= grid.CMax; col++)
            {
                if (visited.Contains((row, col)))
                    continue;
                var cell = grid.GetCell(row, col);
                var (area, visited2) = GetArea((cell.Row, cell.Column), grid);
                var (perimeter, visited3) = GetPerimeter((cell.Row, cell.Column), grid);
                foreach (var v in visited2)
                    visited.Add((v.Row, v.Column));
                total += area * perimeter;
            }
        }


        return total.ToString();
    }

    enum Direction { Left, Right, Up, Down }

    int GetSides(Cell<char> cell, Grid<char> grid)
    {
        int total = 0;
        var flowers = GetGardenPlot((cell.Row, cell.Column), grid);

        var orderedFlowers = flowers.OrderBy(flower => flower.Row).ThenBy(flower => flower.Column);

        HashSet<(int row, int col, Direction direction)> addedFence = [];
        HashSet<(int row, int col)> visited = [];

        foreach (var flower in orderedFlowers)
        {
            int row = flower.Row;
            int column = flower.Column;
            char curr = flower.Value;

            if (grid.grid.TryGetValue((row - 1, column), out Cell<char>? up))
            {
                if (up.Value != curr)
                {
                    if (!addedFence.Contains((row, column - 1, Direction.Up)))
                    {
                        total++;
                        addedFence.Add((row, column, Direction.Up));
                    }
                    else
                    {
                        addedFence.Add((row, column, Direction.Up));
                    }
                }
            }
            else
            {
                if (!addedFence.Contains((row, column - 1, Direction.Up)))
                {
                    total++;
                    addedFence.Add((row, column, Direction.Up));
                }
                else
                {
                    addedFence.Add((row, column, Direction.Up));
                }
            }

            if (grid.grid.TryGetValue((row, column + 1), out Cell<char>? right))
            {
                if (right.Value != curr)
                {
                    if (!addedFence.Contains((row - 1, column, Direction.Right)))
                    {
                        total++;
                        addedFence.Add((row, column, Direction.Right));
                    }
                    else
                    {
                        addedFence.Add((row, column, Direction.Right));
                    }
                }
            }
            else
            {
                if (!addedFence.Contains((row - 1, column, Direction.Right)))
                {
                    total++;
                    addedFence.Add((row, column, Direction.Right));
                }
                else
                {
                    addedFence.Add((row, column, Direction.Right));
                }
            }

            if (grid.grid.TryGetValue((row + 1, column), out Cell<char>? down))
            {
                if (down.Value != curr)
                {
                    if (!addedFence.Contains((row, column - 1, Direction.Down)))
                    {
                        total++;
                        addedFence.Add((row, column, Direction.Down));
                    }
                    else
                    {
                        addedFence.Add((row, column, Direction.Down));
                    }
                }
            }
            else
            {
                if (!addedFence.Contains((row, column - 1, Direction.Down)))
                {
                    total++;
                    addedFence.Add((row, column, Direction.Down));
                }
                else
                {
                    addedFence.Add((row, column, Direction.Down));

                }
            }

            if (grid.grid.TryGetValue((row, column - 1), out Cell<char>? left))
            {
                if (left.Value != curr)
                {
                    if (!addedFence.Contains((row - 1, column, Direction.Left)))
                    {
                        total++;
                        addedFence.Add((row, column, Direction.Left));
                    }
                    else
                    {
                        addedFence.Add((row, column, Direction.Left));
                    }
                }
            }
            else
            {
                if (!addedFence.Contains((row - 1, column, Direction.Left)))
                {
                    total++;
                    addedFence.Add((row, column, Direction.Left));
                }
                else
                {
                    addedFence.Add((row, column, Direction.Left));
                }
            }

        }

        return total;
    }

    public override string PartTwo()
    {
        Grid<char> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                grid.SetCellValue(row, col, c);
            }
        }

        int total = 0;
        HashSet<(int row, int col)> visited = new();
        for (int row = 0; row <= grid.RMax; row++)
        {
            for (int col = 0; col <= grid.CMax; col++)
            {
                if (visited.Contains((row, col)))
                    continue;
                var cell = grid.GetCell(row, col);
                var (area, visited2) = GetArea((cell.Row, cell.Column), grid);
                var perimeter = GetSides(cell, grid);
                Console.WriteLine($"{cell.Value} has {perimeter} sides");
                foreach (var v in visited2)
                    visited.Add((v.Row, v.Column));
                total += area * perimeter;
            }
        }


        return total.ToString();
    }

}