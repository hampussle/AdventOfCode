using Helpers;
using System.Reflection.Metadata;

namespace Solutions.Year2024;

public class Day6(int year, int day) : Day(year, day)
{

    public bool IsObstacle(Grid<Square> grid, (int row, int col) pos)
    {
        return grid.GetCellValue(pos.row, pos.col) is Square.obstacle;
    }

    public bool IsGuard(Grid<Square> grid, (int row, int col) pos)
    {
        var square = grid.GetCellValue(pos.row, pos.col);
        return square is Square.guardDown || square is Square.guardLeft || square is Square.guardUp || square is Square.guardRight;
    }

    public Cell<Square>? Above(Grid<Square> grid, (int row, int col) pos)
    {
        return grid.GetCell(pos.row - 1, pos.col);
    }

    public Cell<Square>? Below(Grid<Square> grid, (int row, int col) pos)
    {
        return grid.GetCell(pos.row + 1, pos.col);
    }

    public Cell<Square>? Left(Grid<Square> grid, (int row, int col) pos)
    {
        return grid.GetCell(pos.row, pos.col - 1);
    }

    public Cell<Square>? Right(Grid<Square> grid, (int row, int col) pos)
    {
        return grid.GetCell(pos.row, pos.col + 1);
    }

    public enum Square { floor, obstacle, guardUp, guardDown, guardLeft, guardRight }
    public enum Direction { Left, Right, Up, Down }

    public (Grid<Square> grid, (int, int) guardPos) ParseGrid()
    {
        Grid<Square> grid = new();

        string[] splitInput = Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var guardPos = (0, 0);

        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                if (c == '.')
                {
                    grid.SetCellValue(row, col, Square.floor);
                }
                else if (c == '#')
                {
                    grid.SetCellValue(row, col, Square.obstacle);
                }
                else if (c == '^')
                {
                    grid.SetCellValue(row, col, Square.guardUp);
                    guardPos = (row, col);
                }
            }
        }

        return (grid, guardPos);
    }

    public HashSet<(int row, int col)> GetTouchedSquares()
    {
        Grid<Square> grid = new();

        string[] splitInput = Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var guardPos = (0, 0);

        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                if (c == '.')
                {
                    grid.SetCellValue(row, col, Square.floor);
                }
                else if (c == '#')
                {
                    grid.SetCellValue(row, col, Square.obstacle);
                }
                else if (c == '^')
                {
                    grid.SetCellValue(row, col, Square.guardUp);
                    guardPos = (row, col);
                }
            }
        }

        HashSet<(int row, int col)> touchedSquares = [];

        while (true)
        {
            touchedSquares.Add(guardPos);
            var guard = grid.GetCellValue(guardPos.Item1, guardPos.Item2);
            if (guard is Square.guardUp)
            {
                var above = Above(grid, guardPos);
                if (above is null)
                {
                    break;
                }
                if (IsObstacle(grid, (above.Row, above.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                }
                else
                {
                    guardPos = (above.Row, above.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                }
            }
            else if (guard is Square.guardDown)
            {
                var below = Below(grid, guardPos);
                if (below is null)
                {
                    break;
                }
                if (IsObstacle(grid, (below.Row, below.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                }
                else
                {
                    guardPos = (below.Row, below.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                }
            }
            else if (guard is Square.guardLeft)
            {
                var left = Left(grid, guardPos);
                if (left is null)
                {
                    break;
                }
                if (IsObstacle(grid, (left.Row, left.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                }
                else
                {
                    guardPos = (left.Row, left.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                }
            }
            else if (guard is Square.guardRight)
            {
                var right = Right(grid, guardPos);
                if (right is null)
                {
                    break;
                }
                if (IsObstacle(grid, (right.Row, right.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                }
                else
                {
                    guardPos = (right.Row, right.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                }
            }
            else
                throw new ArgumentException("guard dir fail");
        }

        return touchedSquares;
    }

    public override string PartOne()
    {
        Grid<Square> grid = new();

        string[] splitInput = Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var guardPos = (0, 0);

        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                char c = line[col];
                if (c == '.')
                {
                    grid.SetCellValue(row, col, Square.floor);
                }
                else if (c == '#')
                {
                    grid.SetCellValue(row, col, Square.obstacle);
                }
                else if (c == '^')
                {
                    grid.SetCellValue(row, col, Square.guardUp);
                    guardPos = (row, col);
                }
            }
        }

        HashSet<(int row, int col)> touchedSquares = [];

        while (true)
        {
            touchedSquares.Add(guardPos);
            var guard = grid.GetCellValue(guardPos.Item1, guardPos.Item2);
            if (guard is Square.guardUp)
            {
                var above = Above(grid, guardPos);
                if (above is null)
                {
                    break;
                }
                if (IsObstacle(grid, (above.Row, above.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                }
                else
                {
                    guardPos = (above.Row, above.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                }
            }
            else if (guard is Square.guardDown)
            {
                var below = Below(grid, guardPos);
                if (below is null)
                {
                    break;
                }
                if (IsObstacle(grid, (below.Row, below.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                }
                else
                {
                    guardPos = (below.Row, below.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                }
            }
            else if (guard is Square.guardLeft)
            {
                var left = Left(grid, guardPos);
                if (left is null)
                {
                    break;
                }
                if (IsObstacle(grid, (left.Row, left.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                }
                else
                {
                    guardPos = (left.Row, left.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                }
            }
            else if (guard is Square.guardRight)
            {
                var right = Right(grid, guardPos);
                if (right is null)
                {
                    break;
                }
                if (IsObstacle(grid, (right.Row, right.Column)))
                {
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                }
                else
                {
                    guardPos = (right.Row, right.Column);
                    grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                }
            }
            else
                throw new ArgumentException("guard dir fail");
        }

        return touchedSquares.Count.ToString();
    }

    public override string PartTwo()
    {
        var (originalGrid, _) = ParseGrid();

        var touchedSquares = GetTouchedSquares();

        int total = 0;

        foreach (var cell in originalGrid.grid)
        {
            if (!touchedSquares.Contains(cell.Key))
                continue;

            bool isLoop = false;

            var (grid, guardPos) = ParseGrid();

            if (cell.Value.Value is not Square.floor)
            {
                continue;
            }
            else
            {
                grid.SetCellValue(cell.Key.r, cell.Key.c, Square.obstacle);
            }

            HashSet<((int, int) pos, Square square)> visited = [];

            while (true)
            {
                var guard = grid.GetCellValue(guardPos.Item1, guardPos.Item2);
                if (visited.Contains((guardPos, guard)))
                {
                    isLoop = true;
                    break;
                }
                visited.Add((guardPos, guard));
                if (guard is Square.guardUp)
                {
                    var above = Above(grid, guardPos);
                    if (above is null)
                    {
                        break;
                    }
                    if (IsObstacle(grid, (above.Row, above.Column)))
                    {
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                    }
                    else
                    {
                        guardPos = (above.Row, above.Column);
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                    }
                }
                else if (guard is Square.guardDown)
                {
                    var below = Below(grid, guardPos);
                    if (below is null)
                    {
                        break;
                    }
                    if (IsObstacle(grid, (below.Row, below.Column)))
                    {
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                    }
                    else
                    {
                        guardPos = (below.Row, below.Column);
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                    }
                }
                else if (guard is Square.guardLeft)
                {
                    var left = Left(grid, guardPos);
                    if (left is null)
                    {
                        break;
                    }
                    if (IsObstacle(grid, (left.Row, left.Column)))
                    {
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardUp);
                    }
                    else
                    {
                        guardPos = (left.Row, left.Column);
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardLeft);
                    }
                }
                else if (guard is Square.guardRight)
                {
                    var right = Right(grid, guardPos);
                    if (right is null)
                    {
                        break;
                    }
                    if (IsObstacle(grid, (right.Row, right.Column)))
                    {
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardDown);
                    }
                    else
                    {
                        guardPos = (right.Row, right.Column);
                        grid.SetCellValue(guardPos.Item1, guardPos.Item2, Square.guardRight);
                    }
                }
                else
                    throw new ArgumentException("guard dir fail");
            }

            if (isLoop)
                total++;
        }

        return total.ToString();
    }

}