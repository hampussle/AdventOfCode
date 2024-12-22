using Helpers;

namespace Solutions.Year2024;

public class Day10(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        Grid<int> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                int number = (int)char.GetNumericValue(line[col]);
                grid.SetCellValue(row, col, number);
            }
        }

        int total = 0;

        foreach (var trails in grid.grid.Where(kvp => kvp.Value.Value == 0))
        {
            Queue<Cell<int>> queue = new();
            HashSet<(int, int)> visited = [];
            queue.Enqueue(trails.Value);

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();

                if (visited.Contains((curr.Row, curr.Column)))
                {
                    continue;
                }

                visited.Add((curr.Row, curr.Column));

                var neighbors = grid.GetNeighboringCellsExcludeDiagonal(curr.Row, curr.Column);

                foreach (var neighbor in neighbors)
                {
                    if (neighbor.Value - curr.Value == 1)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            total += visited.Select(pos => grid.GetCellValue(pos.Item1, pos.Item2)).Count(x => x == 9);
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        Grid<int> grid = new();
        for (int row = 0; row < SplitInput.Length; row++)
        {
            string line = SplitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                int number = (int)char.GetNumericValue(line[col]);
                grid.SetCellValue(row, col, number);
            }
        }

        int total = 0;

        foreach (var trails in grid.grid.Where(kvp => kvp.Value.Value == 0))
        {
            Queue<(Cell<int>, List<(int, int)>)> queue = new();
            HashSet<(int, int, List<(int, int)>)> visited = [];
            queue.Enqueue((trails.Value, []));

            while (queue.Count > 0)
            {
                var (curr, currVisited) = queue.Dequeue();

                if (visited.Contains((curr.Row, curr.Column, currVisited)))
                {
                    continue;
                }

                visited.Add((curr.Row, curr.Column, currVisited));

                var neighbors = grid.GetNeighboringCellsExcludeDiagonal(curr.Row, curr.Column);

                foreach (var neighbor in neighbors)
                {
                    if (neighbor.Value - curr.Value == 1)
                    {
                        List<(int, int)> newCurrVisited = new(currVisited);
                        newCurrVisited.Add((curr.Row, curr.Column));
                        queue.Enqueue((neighbor, new(currVisited)));
                    }
                }
            }

            total += visited.Select(pos => grid.GetCellValue(pos.Item1, pos.Item2)).Count(x => x == 9);
        }

        return total.ToString();
    }

}