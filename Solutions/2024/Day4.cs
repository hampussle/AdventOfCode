using Helpers;

namespace Solutions.Year2024;

public class Day4(int year, int day) : Day(year, day)
{
    bool IsXmas(string str) => str == "XMAS" || str == "SAMX";

    int MatchColumn(Grid<char> grid, int row, int col)
    {
        // Up
        var x = grid.GetCellValue(row, col);
        var m = grid.GetCellValue(row - 1, col);
        var a = grid.GetCellValue(row - 2, col);
        var s = grid.GetCellValue(row - 3, col);
        string up = string.Concat(x, m, a, s);

        // Down
        var x1 = grid.GetCellValue(row, col);
        var m1 = grid.GetCellValue(row + 1, col);
        var a1 = grid.GetCellValue(row + 2, col);
        var s1 = grid.GetCellValue(row + 3, col);
        string down = string.Concat(x1, m1, a1, s1);

        int total = 0;
        //if (IsXmas(up))
        //    total++;
        if (IsXmas(down))
            total++;
        return total;
    }

    int MatchRow(Grid<char> grid, int row, int col)
    {
        // Right
        var x = grid.GetCellValue(row, col);
        var m = grid.GetCellValue(row, col + 1);
        var a = grid.GetCellValue(row, col + 2);
        var s = grid.GetCellValue(row, col + 3);
        string right = string.Concat(x, m, a, s);

        // Left
        var x1 = grid.GetCellValue(row, col);
        var m1 = grid.GetCellValue(row, col - 1);
        var a1 = grid.GetCellValue(row, col - 2);
        var s1 = grid.GetCellValue(row, col - 3);
        string left = string.Concat(x1, m1, a1, s1);

        int total = 0;
        //if (IsXmas(left))
        //    total++;
        if (IsXmas(right))
            total++;
        return total;
    }

    int MatchDiagonal(Grid<char> grid, int row, int col)
    {
        // left up
        var x = grid.GetCellValue(row, col);
        var m = grid.GetCellValue(row - 1, col - 1);
        var a = grid.GetCellValue(row - 2, col - 2);
        var s = grid.GetCellValue(row - 3, col - 3);
        string leftUp = string.Concat(x, m, a, s);

        // right up
        var x1 = grid.GetCellValue(row, col);
        var m1 = grid.GetCellValue(row - 1, col + 1);
        var a1 = grid.GetCellValue(row - 2, col + 2);
        var s1 = grid.GetCellValue(row - 3, col + 3);
        string rightUp = string.Concat(x1, m1, a1, s1);

        // right down
        var x2 = grid.GetCellValue(row, col);
        var m2 = grid.GetCellValue(row + 1, col + 1);
        var a2 = grid.GetCellValue(row + 2, col + 2);
        var s2 = grid.GetCellValue(row + 3, col + 3);
        string rightDown = string.Concat(x2, m2, a2, s2);

        // left down
        var x4 = grid.GetCellValue(row, col);
        var m4 = grid.GetCellValue(row + 1, col - 1);
        var a4 = grid.GetCellValue(row + 2, col - 2);
        var s4 = grid.GetCellValue(row + 3, col - 3);
        string leftDown = string.Concat(x4, m4, a4, s4);

        int total = 0;
        //if (IsXmas(leftUp))
        //    total++;
        //if (IsXmas(rightUp))
        //    total++;
        if (IsXmas(leftDown))
            total++;
        if (IsXmas(rightDown))
            total++;

        return total;
    }

    bool MatchMas(Grid<char> grid, int row, int col)
    {
        var topLeft = grid.GetCellValue(row, col);
        var topRight = grid.GetCellValue(row, col + 2);
        var mid = grid.GetCellValue(row + 1, col + 1);
        var bottomLeft = grid.GetCellValue(row + 2, col);
        var bottomRight = grid.GetCellValue(row + 2, col + 2);

        if (topLeft == 'M')
        {
            if (mid == 'A')
            {
                if (bottomRight == 'S')
                {
                    if (topRight == 'M')
                    {
                        if (bottomLeft == 'S')
                        {
                            return true;
                        }
                    }
                    else if (topRight == 'S')
                    {
                        if (bottomLeft == 'M')
                        {
                            return true;
                        }
                    }
                }
            }
        }
        else if (topLeft == 'S')
        {
            if (mid == 'A')
            {
                if (bottomRight == 'M')
                {
                    if (topRight == 'M')
                    {
                        if (bottomLeft == 'S')
                        {
                            return true;
                        }
                    }
                    else if (topRight == 'S')
                    {
                        if (bottomLeft == 'M')
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public override string PartOne()
    {
        Grid<char> grid = new();
        string[] splitInput = SplitInput;

        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                grid.SetCellValue(row, col, line[col]);
            }
        }

        int total = 0;

        foreach (var kvp in grid.grid)
        {
            (int row, int col) = kvp.Key;

            total += MatchDiagonal(grid, row, col);
            total += MatchRow(grid, row, col);
            total += MatchColumn(grid, row, col);
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        Grid<char> grid = new();
        string[] splitInput = Input.Split('\n').SkipLast(1).ToArray();

        for (int row = 0; row < splitInput.Length; row++)
        {
            string line = splitInput[row];
            for (int col = 0; col < line.Length; col++)
            {
                grid.SetCellValue(row, col, line[col]);
            }
        }

        int total = 0;

        foreach (var kvp in grid.grid)
        {
            (int row, int col) = kvp.Key;

            if (MatchMas(grid, row, col))
                total++;

        }

        return total.ToString();
    }

}