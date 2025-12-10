using Helpers;
using System.Drawing;

namespace Solutions.Year2025;

public class Day9(int year, int day) : Day(year, day)
{

    static int Distance(Point a, Point b) => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    static long Area(Point a, Point b)
    {
        var width = Math.Max(a.X, b.X) - Math.Min(a.X, b.X) + 1;
        var height = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y) + 1;
        return (long)width * (long)height;
    }

    public override string PartOne()
    {
        var points = SplitInput.Select(line => {
            var nums = line.Split(',').Select(int.Parse);
            return new Point(nums.First(), nums.Last());
        }).ToArray();

        (int distance, Point a, Point b) biggest = (int.MinValue, Point.Empty, Point.Empty);
        for (int i = 0; i < points.Length; i++)
        {
            Point point = points[i];
            for (int j = i + 1; j < points.Length; j++)
            {
                Point other = points[j];
                int distance = Distance(point, other);
                if (distance > biggest.distance)
                    biggest = (distance, point, other);
            }
        }

        var (dist, a, b) = biggest;

        return Area(a, b).ToString();
    }

    public override string PartTwo()
    {
        var points = SplitInput.Select(line => {
            var nums = line.Split(',').Select(int.Parse);
            return new Point(nums.First(), nums.Last());
        }).ToArray();

        List<(int area, Point a, Point b)> areas = [];
        for (int i = 0; i < points.Length; i++)
        {
            Console.WriteLine($"{i} / {points.Length}");
            Point point = points[i];
            for (int j = points.Length - 1; j >= i + 1; j--)
            {
                Console.WriteLine($"{i} / {points.Length}: {j} / {points.Length}");
                Point other = points[j];
                int distance = Distance(point, other);
                areas.Add((distance, point, other));
            }
        }

        areas = [.. areas.OrderByDescending(d => d.area)];

        int counter = 0;
        int count = areas.Count;
        foreach (var (_, a, b) in areas)
        {
            counter++;
            Console.WriteLine($"{counter} / {count}");
            if (ValidRectangle(a, b))
                return Area(a, b).ToString();
        }

        return "fail";

        bool ValidRectangle(Point a, Point b)
        {
            var (maxX, minX) = a.X > b.X ? (a.X, b.X) : (b.X, a.X);
            var (maxY, minY) = a.Y > b.Y ? (a.Y, b.Y) : (b.Y, a.Y);

            if (!validPoint2(new Point(minX, minY)) ||
                !validPoint2(new Point(minX, maxY)) ||
                !validPoint2(new Point(maxX, minY)) ||
                !validPoint2(new Point(maxX, maxY)))
                return false;

            for (int col = minX; col <= maxX; col++)
            {
                if (!ValidUp(new Point(col, minY)))
                    return false;
                if (!ValidDown(new Point(col, maxY)))
                    return false;
            }
            for (int row = minY; row <= maxY; row++)
            {
                if (!ValidLeft(new Point(minX, row)))
                    return false;
                if (!ValidRight(new Point(maxX, row)))
                    return false;
            }

            return true;

            bool ValidRight(Point point)
            {
                return points.Contains(point) ||
                    (points.Any(p => p.Y == point.Y && p.X < point.X) && points.Any(p => p.Y == point.Y && p.X > point.X)) ||
                    (points.Any(p => p.X == point.X && p.Y < point.Y) && points.Any(p => p.X == point.X && p.Y > point.Y)) ||
                    points.Any(p => p.X > point.X && (p.Y == point.Y || (p.Y < point.Y && points.Any(p2 => p2.X == p.X && p2.Y > point.Y))));
            }

            bool ValidLeft(Point point)
            {
                return points.Contains(point) ||
                    (points.Any(p => p.Y == point.Y && p.X < point.X) && points.Any(p => p.Y == point.Y && p.X > point.X)) ||
                    (points.Any(p => p.X == point.X && p.Y < point.Y) && points.Any(p => p.X == point.X && p.Y > point.Y)) ||
                    points.Any(p => p.X < point.X && (p.Y == point.Y || (p.Y < point.Y && points.Any(p2 => p2.X == p.X && p2.Y > point.Y))));
            }

            bool ValidUp(Point point)
            {
                return points.Contains(point) ||
                    (points.Any(p => p.Y == point.Y && p.X < point.X) && points.Any(p => p.Y == point.Y && p.X > point.X)) ||
                    (points.Any(p => p.X == point.X && p.Y < point.Y) && points.Any(p => p.X == point.X && p.Y > point.Y)) ||
                    (points.Any(p => p.Y < point.Y && (p.X == point.X || (p.X < point.X && points.Any(p2 => p2.Y == p.Y && p2.X > point.X)))));
            }

            bool ValidDown(Point point)
            {
                return points.Contains(point) ||
                    (points.Any(p => p.Y == point.Y && p.X < point.X) && points.Any(p => p.Y == point.Y && p.X > point.X)) ||
                    (points.Any(p => p.X == point.X && p.Y < point.Y) && points.Any(p => p.X == point.X && p.Y > point.Y)) ||
                    points.Any(p => p.Y > point.Y && (p.X == point.X || (p.X < point.X && points.Any(p2 => p2.Y == p.Y && p2.X > point.X))));
            }

            bool validPoint2(Point point)
            {
                return points.Contains(point) ||
                    (points.Any(p => p.Y == point.Y && p.X < point.X) && points.Any(p => p.Y == point.Y && p.X > point.X)) ||
                    (points.Any(p => p.X == point.X && p.Y < point.Y) && points.Any(p => p.X == point.X && p.Y > point.Y)) ||
                    (points.Any(p => p.Y < point.Y && (p.X == point.X || (p.X < point.X && points.Any(p2 => p2.Y == p.Y && p2.X > point.X))))
                    && points.Any(p => p.Y > point.Y && (p.X == point.X || (p.X < point.X && points.Any(p2 => p2.Y == p.Y && p2.X > point.X))))
                    && points.Any(p => p.X < point.X && (p.Y == point.Y || (p.Y < point.Y && points.Any(p2 => p2.X == p.X && p2.Y > point.Y))))
                    && points.Any(p => p.X > point.X && (p.Y == point.Y || (p.Y < point.Y && points.Any(p2 => p2.X == p.X && p2.Y > point.Y)))));
            }
        }
    }

}