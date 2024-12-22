namespace Helpers;

public class Cell<T>(int row, int column, T value)
{
    public int Row { get; } = row;
    public int Column { get; } = column;
    public T Value { get; set; } = value;
}

public class Grid<T>
{
    public Dictionary<(int r, int c), Cell<T>> grid;
    public int RMax => grid.Max(x => x.Key.r);
    public int CMax => grid.Max(x => x.Key.c);
    public int RMin => grid.Min(x => x.Key.r);
    public int CMin => grid.Min(x => x.Key.c);

    public Grid()
    {
        grid = [];
    }

    public void SetCellValue(int row, int column, T value)
    {
        var key = (row, column);
        if (grid.TryGetValue(key, out Cell<T>? val))
        {
            val.Value = value;
        }
        else
        {
            Cell<T> cell = new(row, column, value);
            grid[key] = cell;
        }
    }

    public Cell<T>? GetCell(int row, int column)
    {
        var key = (row, column);
        if (grid.TryGetValue(key, out Cell<T>? value))
        {
            return value;
        }
        return default;
    }

    public T? GetCellValue(int row, int column)
    {
        var key = (row, column);
        if (grid.TryGetValue(key, out Cell<T>? value))
        {
            return value.Value;
        }
        return default;
    }

    public void PrintGrid()
    {
        int rMax = grid.Max(x => x.Key.r);
        int cMax = grid.Max(x => x.Key.c);
        for (int r = 0; r <= rMax; r++)
        {
            for (int c = 0; c <= cMax; c++)
            {
                Console.Write(grid[(r, c)].Value);
            }
            Console.WriteLine();
        }
    }

    public void PrintGrid(Func<T, string> toString)
    {
        int rMax = grid.Max(x => x.Key.r);
        int cMax = grid.Max(x => x.Key.c);
        for (int r = 0; r <= rMax; r++)
        {
            for (int c = 0; c <= cMax; c++)
            {
                Console.Write(toString(grid[(r, c)].Value));
            }
            Console.WriteLine();
        }
    }

    public List<Cell<T>> GetNeighboringCells(int row, int column)
    {
        List<Cell<T>> neighbors = [];

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (IsValidCell(i, j) && (i != row || j != column))
                {
                    var key = (i, j);
                    if (grid.TryGetValue(key, out Cell<T>? value))
                    {
                        neighbors.Add(value);
                    }
                }
            }
        }

        return neighbors;
    }

    public List<Cell<T>> GetNeighboringCellsExcludeDiagonal(int row, int column)
    {
        List<Cell<T>> neighbors = [];
        if (grid.TryGetValue((row - 1, column), out Cell<T>? up))
            neighbors.Add(up);
        if (grid.TryGetValue((row, column + 1), out Cell<T>? right))
            neighbors.Add(right);
        if (grid.TryGetValue((row + 1, column), out Cell<T>? down))
            neighbors.Add(down);
        if (grid.TryGetValue((row, column - 1), out Cell<T>? left))
            neighbors.Add(left);
        return neighbors;
    }

    public List<Cell<T>> GetNeighboringCellsExcludeDiagonal(int row, int column, Func<Cell<T>, bool> condition)
    {
        List<Cell<T>> neighbors = [];
        if (grid.TryGetValue((row - 1, column), out Cell<T>? up))
            neighbors.Add(up);
        if (grid.TryGetValue((row, column + 1), out Cell<T>? right))
            neighbors.Add(right);
        if (grid.TryGetValue((row + 1, column), out Cell<T>? down))
            neighbors.Add(down);
        if (grid.TryGetValue((row, column - 1), out Cell<T>? left))
            neighbors.Add(left);
        return neighbors.Where(condition).ToList();
    }

    public List<T> GetDiagonalLeftTop(int row, int column, bool inclusive)
    {
        List<T> diagonalValues = [];

        if (!inclusive)
        {
            row--;
            column--;
        }

        while (grid.TryGetValue((row, column), out Cell<T>? value))
        {
            diagonalValues.Add(value.Value);
            row--;
            column--;
        }

        return diagonalValues;
    }

    public List<T> GetDiagonalLeftBottom(int row, int column, bool inclusive)
    {
        List<T> diagonalValues = [];

        if (!inclusive)
        {
            row++;
            column--;
        }

        while (grid.TryGetValue((row, column), out Cell<T>? value))
        {
            diagonalValues.Add(value.Value);
            row++;
            column--;
        }

        return diagonalValues;
    }

    public List<T> GetDiagonalRightTop(int row, int column, bool inclusive)
    {
        List<T> diagonalValues = [];

        if (!inclusive)
        {
            row--;
            column++;
        }

        while (grid.TryGetValue((row, column), out Cell<T>? value))
        {
            diagonalValues.Add(value.Value);
            row--;
            column++;
        }

        return diagonalValues;
    }

    public List<T> GetDiagonalRightBottom(int row, int column, bool inclusive)
    {
        List<T> diagonalValues = [];

        if (!inclusive)
        {
            row++;
            column++;
        }

        while (grid.TryGetValue((row, column), out Cell<T>? value))
        {
            diagonalValues.Add(value.Value);
            row++;
            column++;
        }

        return diagonalValues;
    }

    public List<T> GetRowValues(int row)
    {
        List<T> rowValues = [];

        for (int j = 0; j < grid.Count; j++)
        {
            var key = (row, j);
            if (grid.TryGetValue(key, out Cell<T>? value))
            {
                rowValues.Add(value.Value);
            }
        }

        return rowValues;
    }

    public List<T> GetColumnValues(int column)
    {
        List<T> columnValues = [];

        for (int i = 0; i < grid.Count; i++)
        {
            var key = (i, column);
            if (grid.TryGetValue(key, out Cell<T>? value))
            {
                columnValues.Add(value.Value);
            }
        }

        return columnValues;
    }

    public void Transpose()
    {
        Dictionary<(int, int), Cell<T>> transposedGrid = [];

        foreach (var cell in grid.Values)
        {
            var transposedKey = (cell.Column, cell.Row);
            Cell<T> transposedCell = new(cell.Column, cell.Row, cell.Value);
            transposedGrid[transposedKey] = transposedCell;
        }

        grid = transposedGrid;
    }

    public void ClearGrid()
    {
        grid.Clear();
    }

    public bool IsValidCell(int row, int column)
    {
        return row >= 0 && row < grid.Count && column >= 0 && column < grid.Count;
    }

    public void FloodFill(int startRow, int startColumn, T fillValue)
    {
        var startKey = (startRow, startColumn);
        if (!grid.TryGetValue(startKey, out Cell<T>? value))
        {
            throw new InvalidOperationException("Cannot perform flood fill on an empty cell.");
        }

        T originalValue = value.Value;

        // Use a stack to perform iterative flood fill
        Stack<(int r, int c)> stack = new();
        stack.Push(startKey);

        while (stack.Count > 0)
        {
            (int r, int c) currentKey = stack.Pop();
            int currentRow = currentKey.r;
            int currentColumn = currentKey.c;
            if (!IsValidCell(currentRow, currentColumn))
            {
                continue;
            }
            if (!grid.TryGetValue(currentKey, out Cell<T>? val) || !EqualityComparer<T>.Default.Equals(val.Value, originalValue))
            {
                continue;
            }

            grid[currentKey].Value = fillValue;

            // Add neighboring cells to the stack for further processing
            stack.Push((currentRow - 1, currentColumn));
            stack.Push((currentRow + 1, currentColumn));
            stack.Push((currentRow, currentColumn - 1));
            stack.Push((currentRow, currentColumn + 1));
        }
    }
}
