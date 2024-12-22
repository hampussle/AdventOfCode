using Helpers;
using System.Drawing;
using System.Linq;

namespace Solutions.Year2024;

public class Day14(int year, int day) : Day(year, day)
{

    int CMax;
    int CMin;
    int RMax;
    int RMin;

    class Robot(Point startPos, Point velocity)
    {
        public Point StartPos = startPos;
        public Point Velocity = velocity;
        public Point CurrentPos = startPos;
    }

    Grid<List<Robot>> CreateEmptyGrid(int rowSize, int colSize)
    {
        Grid<List<Robot>> grid = new();
        for (int row = 0; row < rowSize; row++)
        {
            for (int col = 0; col < colSize; col++)
            {
                grid.SetCellValue(row, col, new());
            }
        }
        return grid;
    }

    List<Robot> GetRobots()
    {
        // p=0,4 v=3,-3
        List<Robot> robots = [];
        foreach (string line in SplitInput)
        {
            int[] parts = line.RemoveFromString("p=").Replace(" v=", ",").Split(',').Select(int.Parse).ToArray();
            robots.Add(new(new(parts[0], parts[1]), new(parts[2], parts[3])));
        }
        return robots;
    }

    Grid<List<Robot>> MoveOne(Grid<List<Robot>> grid)
    {
        foreach (var cell in grid.grid)
        {
            if (cell.Value is null || cell.Value.Value.Count == 0)
                continue;

            var robots = cell.Value.Value;
            foreach (var robot in robots)
            {
                int newX = robot.StartPos.X;
            }
        }

        return grid;
    }

    List<Robot> MoveOne(List<Robot> robots)
    {
        foreach (var robot in robots)
        {
            int newX = robot.CurrentPos.X + robot.Velocity.X;

            if (newX == CMax)
            {
                newX = 0;
            }
            else if (newX > CMax)
            {
                newX -= CMax;
            }
            else if (newX == 0)
            {
                //newX = CMax;
            }
            else if (newX < 0)
            {
                newX = CMax + newX;
            }

            int newY = robot.CurrentPos.Y + robot.Velocity.Y;
            if (newY == RMax)
            {
                newY = 0;
            }
            else if (newY > RMax)
            {
                newY -= RMax;
            }
            else if (newY == 0)
            {
                //newY = RMax;
            }
            else if (newY < 0)
            {
                newY = RMax + newY;
            }

            robot.CurrentPos = new(newX, newY);
        }
        return robots;
    }

    List<Grid<List<Robot>>> GetQuadrants(Grid<List<Robot>> grid)
    {
        int quadrantWidth = grid.CMax / 2;
        int quadrantHeight = grid.RMax / 2;

        List<Grid<List<Robot>>> quadrants = [];

        Grid<List<Robot>> upperLeftQuadrant = new();
        for (int row = 0; row < quadrantHeight; row++)
        {
            for (int col = 0; col < quadrantWidth; col++)
            {
                upperLeftQuadrant.SetCellValue(row, col, grid.GetCellValue(row, col));
            }
        }
        quadrants.Add(upperLeftQuadrant);

        Grid<List<Robot>> upperRightQuadrant = new();
        for (int row = 0; row < quadrantHeight; row++)
        {
            for (int col = 0; col < quadrantWidth; col++)
            {
                upperRightQuadrant.SetCellValue(row, col, grid.GetCellValue(row, col));
            }
        }
        quadrants.Add(upperRightQuadrant);

        Grid<List<Robot>> lowerLeftQuadrant = new();
        for (int row = 0; row < quadrantHeight; row++)
        {
            for (int col = 0; col < quadrantWidth; col++)
            {
                lowerLeftQuadrant.SetCellValue(row, col, grid.GetCellValue(row, col));
            }
        }
        quadrants.Add(lowerLeftQuadrant);

        Grid<List<Robot>> lowerRightQuadrant = new();
        for (int row = 0; row < quadrantHeight; row++)
        {
            for (int col = 0; col < quadrantWidth; col++)
            {
                lowerRightQuadrant.SetCellValue(row, col, grid.GetCellValue(row, col));
            }
        }
        quadrants.Add(lowerRightQuadrant);

        return quadrants;
    }

    int GetSafetyFactor(Grid<List<Robot>> grid)
    {
        var quadrants = GetQuadrants(grid);

        int total = 0;

        foreach (var quadrant in quadrants)
        {
            int robots = 0;
            foreach (var cell in quadrant.grid)
            {
                robots += cell.Value.Value.Count;
            }
            total += robots;
        }

        return total;
    }

    int GetSafetyFactor(List<Robot> robots)
    {
        int quadrantWidth = CMax / 2;
        int quadrantHeight = RMax / 2;

        List<Robot> upperLeftQuadrant = robots.Where(rb => rb.CurrentPos.X < quadrantWidth && rb.CurrentPos.Y < quadrantHeight).ToList();
        List<Robot> upperRightQuadrant = robots.Where(rb => rb.CurrentPos.X > quadrantWidth && rb.CurrentPos.Y < quadrantHeight).ToList();
        List<Robot> lowerLeftQuadrant = robots.Where(rb => rb.CurrentPos.X < quadrantWidth && rb.CurrentPos.Y > quadrantHeight).ToList();
        List<Robot> lowerRightQuadrant = robots.Where(rb => rb.CurrentPos.X > quadrantWidth && rb.CurrentPos.Y > quadrantHeight).ToList();

        List<List<Robot>> quadrants = [upperLeftQuadrant, upperRightQuadrant, lowerLeftQuadrant, lowerRightQuadrant];

        int total = 1;

        foreach (var quadrant in quadrants)
        {
            Console.WriteLine("quadrant " + quadrant.Count);
            total *= quadrant.Count;
        }

        return total;
    }

    public override string PartOne()
    {
        //var grid = UseTestInput ? CreateEmptyGrid(7, 11) : CreateEmptyGrid(101, 103);

        if (UseTestInput)
        {
            RMax = 7;
            RMin = 0;
            CMax = 11;
            CMin = 0;
        }
        else
        {
            RMax = 103;
            RMin = 0;
            CMax = 101;
            CMin = 0;
        }

        var robots = GetRobots();

        //foreach (var robot in robots)
        //{
        //    int row = robot.StartPos.Y;
        //    int col = robot.StartPos.X;

        //    grid.GetCellValue(row, col)!.Add(robot);
        //}
        //PrintGrid(robots);
        //Console.WriteLine();

        for (int i = 0; i < 100; i++)
        {
            MoveOne(robots);
            //PrintGrid(robots);
            Console.WriteLine($"{i + 1} / {100}");
        }

        //PrintGrid(robots);

        int result = GetSafetyFactor(robots);

        return result.ToString();
    }

    public override string PartTwo()
    {
        RMax = 103;
        RMin = 0;
        CMax = 101;
        CMin = 0;

        var robots = GetRobots();

        long seconds = 0;
        while (!NoRobotsAreOverlapping(robots))
        {
            seconds++;
            MoveOne(robots);
        }

        PrintGrid(robots);

        return seconds.ToString();
    }

    bool AllRobotsAreConnected(List<Robot> robots)
    {
        Grid<Robot?> grid = new();
        for (int row = 0; row <= RMax; row++)
        {
            for (int col = 0; col <= CMax; col++)
            {
                var robot = robots.FirstOrDefault(rb => rb.CurrentPos.X == col && rb.CurrentPos.Y == row);
                grid.SetCellValue(row, col, robot);
            }
        }

        foreach (var robot in robots)
        {
            int row = robot.CurrentPos.Y;
            int col = robot.CurrentPos.X;

            var neighbors = grid.GetNeighboringCells(row, col);
            if (neighbors.Count == 0)
                return true;
        }

        return false;
    }

    bool NoRobotsAreOverlapping(List<Robot> robots)
    {
        HashSet<Point> visited = [];
        foreach (var robot in robots)
        {
            int row = robot.CurrentPos.Y;
            int col = robot.CurrentPos.X;

            if (visited.Contains(new(row, col)))
                continue;

            if (robots.Where(rb => rb.CurrentPos.Y == row && rb.CurrentPos.X == col).Count() > 1)
                return false;

            visited.Add(new(row, col));
        }

        return true;
    }

    void PrintGrid(List<Robot> robots)
    {
        for (int row = 0; row < RMax; row++)
        {
            for (int col = 0; col < CMax; col++)
            {
                var robotsCount = robots.Where(rb => rb.CurrentPos.X == col && rb.CurrentPos.Y == row).Count();
                if (robotsCount == 0)
                    Console.Write('.');
                else
                    Console.Write(robotsCount);
            }
            Console.WriteLine();
        }
    }

}