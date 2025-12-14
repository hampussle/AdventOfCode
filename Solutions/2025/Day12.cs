using Helpers;

namespace Solutions.Year2025;

public class Day12(int year, int day) : Day(year, day)
{

    sealed class Region
    {
        public Region(int width, int height, int[] presents)
        {
            Presents = presents;
            Grid = new();
            for (int col = 0; col < width; col++)
                for (int row = 0; row < height; row++)
                    Grid.SetCellValue(row, col, false);
        }

        public Grid<bool> Grid { get; init; }
        public int[] Presents { get; init; }

    }

    sealed class Present
    {
        public bool[][] Shape { get; init; }

        public Present(bool[][] shape)
        {
            Shape = shape;
        }

        public void Rotate()
        {
            var height = Shape.Length;
            var width = Shape[0].Length;
            var newHeight = width;
            var newWidth = height - (height + 1);
            var shape = Array.Empty<bool[]>();

            for (int col = 0; col < width; col++)
                for (int row = 0; row < height; row++)
                    shape[newHeight][newWidth] = Shape[height][width];
        }
    }

    public override string PartOne()
    {
        var presentTypes = new List<Present>();
        var regions = new List<Region>();

        var lines = new List<bool[]>();
        for (int i = 0; i < SplitInput.Length; i++)
        {
            string? line = SplitInput[i];
            if (line.Contains('x'))
            {
                regions.Add(new(int.Parse(line.TakeWhile(c => c != 'x').ConcatChars()), int.Parse(line.Split('x')[1].TakeWhile(char.IsNumber).ConcatChars()), [.. line[(line.IndexOf(": ") + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)]));
                continue;
            }
            if (char.IsNumber(line[0]))
                continue;

            lines.Add([.. line.Select(c => c == '#')]);
            if (lines.Count == 3)
            {
                presentTypes.Add(new([.. lines]));
                lines.Clear();
            }
        }

        int total = 0;

        foreach (var region in regions)
        {
            var presents = new List<Present>();
            for (int i = 0; i < region.Presents.Length; i++)
                for (int j = 0; j < region.Presents[i]; j++)
                    presents.Add(presentTypes[i]);
            var gridSize = (region.Grid.CMax + 1) * (region.Grid.RMax + 1);
            var presentsSize = presents.Sum(present => present.Shape.Sum(arr => arr.Sum(b => b ? 1 : 0)));
            if (gridSize > presentsSize * 1.2)
            {
                Console.WriteLine($"Accepted: total: {gridSize} - presents: {presentsSize}");
                total++;
            }
            else if (gridSize < presentsSize)
                Console.WriteLine($"Rejected: total: {gridSize} - presents: {presentsSize}");
            else
                Console.WriteLine($"Rejected: total: {gridSize} - presents: {presentsSize}");
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        return base.PartTwo();
    }

}