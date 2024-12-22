using Helpers;
using System.Drawing;

namespace Solutions.Year2024;

public class Day18(int year, int day) : Day(year, day)
{

    Grid<bool> GetGrid(int size)
    {
        Grid<bool> grid = new();
        for (int row = 0; row <= size; row++)
        {
            for (int col = 0; col <= size; col++)
            {
                grid.SetCellValue(row, col, true);
            }
        }
        return grid;
    }

    List<Point> GetBytes()
    {
        return SplitInput.Select(line => line.Split(',').Select(int.Parse).ToPoint()).ToList();
    }

    Grid<bool> SendBytes(List<Point> bytes, Grid<bool> grid, int nr)
    {
        for (int i = 0; i < nr; i++)
        {
            var corrupted = bytes[i];
            grid.SetCellValue(corrupted.Y, corrupted.X, false);
        }
        return grid;
    }

    public override string PartOne()
    {
        var grid = GetGrid(UseTestInput ? 6 : 70);
        var bytes = GetBytes();
        grid = UseTestInput ? SendBytes(bytes, grid, 12) : SendBytes(bytes, grid, 1024);

        grid.PrintGrid((bool b) => b ? "." : "#");

        Point start = new(0, 0);
        Point end = UseTestInput ? new(6, 6) : new(70, 70);

        PriorityQueue<Point, int> queue = new();
        Dictionary<Point, int> distances = [];

        queue.Enqueue(start, 0);

        while (queue.TryDequeue(out var curr, out int steps))
        {
            if (distances.TryGetValue(curr, out int minSteps) && minSteps <= steps)
                continue;

            distances[curr] = steps;

            if (curr == end)
            {
                return steps.ToString();
            }

            var neighbors = grid.GetNeighboringCellsExcludeDiagonal(curr.Y, curr.X);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Value)
                {
                    queue.Enqueue(new(neighbor.Column, neighbor.Row), steps + 1);
                }
            }
        }


        return base.PartOne();
    }

    public override string PartTwo()
    {
        var grid = GetGrid(UseTestInput ? 6 : 70);
        var bytes = GetBytes();

        for (int i = 0; i < 1_000_000; i++)
        {
            Console.WriteLine($"{i} / {bytes.Count}");

            grid = UseTestInput ? SendBytes(bytes, grid, i) : SendBytes(bytes, grid, i);

            //grid.PrintGrid((bool b) => b ? "." : "#");

            Point start = new(0, 0);
            Point end = UseTestInput ? new(6, 6) : new(70, 70);

            PriorityQueue<Point, int> queue = new();
            Dictionary<Point, int> distances = [];

            queue.Enqueue(start, 0);

            bool foundExit = false;

            while (queue.TryDequeue(out var curr, out int steps))
            {
                if (distances.TryGetValue(curr, out int minSteps) && minSteps <= steps)
                    continue;

                distances[curr] = steps;

                if (curr == end)
                {
                    foundExit = true;
                    break;
                }

                var neighbors = grid.GetNeighboringCellsExcludeDiagonal(curr.Y, curr.X);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor.Value)
                    {
                        queue.Enqueue(new(neighbor.Column, neighbor.Row), steps + 1);
                    }
                }
            }

            if (!foundExit)
            {
                return bytes[i - 1].ToString();
            }
        }

        return base.PartTwo();
    }

}