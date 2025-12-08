using Helpers;

namespace Solutions.Year2025;

public class Day8(int year, int day) : Day(year, day)
{

    readonly struct Box
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }

        public Box(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Distance(Box other)
        {
            return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2));
        }
    }



    public override string PartOne()
    {
        var boxes = SplitInput
                    .Select(line =>
                        line.Split(',')
                            .Select(int.Parse)
                            .ToArray())
                    .Select(arr => new Box(arr[0], arr[1], arr[2]))
                    .ToArray();

        List<List<Box>> circuits = [];
        Dictionary<Box, List<Box>> connections = [];

        int cycles = UseTestInput ? 10 : 1000;
        for (int cycle = 0; cycle < cycles; cycle++)
        {
            (double distance, Box, Box) smallestDistance = (double.MaxValue, boxes[0], boxes[0]);
            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    var first = boxes[i];
                    var second = boxes[j];
                    if (connections.TryGetValue(first, out var directConnections) && directConnections.Contains(second))
                        continue;
                    if (connections.TryGetValue(second, out directConnections) && directConnections.Contains(first))
                        continue;
                    var distance = first.Distance(second);
                    if (distance < smallestDistance.distance)
                        smallestDistance = (distance, first, second);
                }
            }
            var (_, boxOne, boxTwo) = smallestDistance;
            if (!connections.TryGetValue(boxOne, out List<Box>? boxOneConnections))
                connections[boxOne] = [boxTwo];
            else
                boxOneConnections.Add(boxTwo);

            var boxOneCircuit = circuits.FirstOrDefault(circuit => circuit.Contains(boxOne));
            var boxTwoCircuit = circuits.FirstOrDefault(circuit => circuit.Contains(boxTwo));

            if (boxOneCircuit is not null && boxTwoCircuit is not null)
            {
                if (boxOneCircuit == boxTwoCircuit)
                    continue;
                circuits.Remove(boxTwoCircuit);
                boxOneCircuit.AddRange(boxTwoCircuit);
            }
            else if (boxOneCircuit is null && boxTwoCircuit is null)
                circuits.Add([boxOne, boxTwo]);
            else if (boxOneCircuit is not null && boxTwoCircuit is null)
                boxOneCircuit.Add(boxTwo);
            else if (boxOneCircuit is null && boxTwoCircuit is not null)
                boxTwoCircuit.Add(boxOne);
        }

        return circuits
            .Select(circuit => circuit.Count)
            .OrderDescending()
            .Take(3)
            .Aggregate(1L, (a, b) => a * b)
            .ToString();
    }

    public override string PartTwo()
    {
        var boxes = SplitInput
            .Select(line =>
                line.Split(',')
                    .Select(int.Parse)
                    .ToArray())
            .Select(arr => new Box(arr[0], arr[1], arr[2]))
            .ToArray();

        List<List<Box>> circuits = [];

        List<(double distance, Box, Box)> pairs = [];

        for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = i + 1; j < boxes.Length; j++)
            {
                var first = boxes[i];
                var second = boxes[j];
                var distance = first.Distance(second);
                pairs.Add((distance, first, second));
            }
        }

        pairs = [.. pairs.OrderBy(pair => pair.distance)];

        while (true)
        {
            Box boxOne = default;
            Box boxTwo = default;
            for (int i = 0; i < pairs.Count; i++)
            {
                var (_, first, second) = pairs[i];
                if (circuits.Any(circuit => circuit.Contains(first) && circuit.Contains(second)))
                    continue;
                (boxOne, boxTwo) = (first, second);
                pairs.RemoveAt(i);
                break;
            }

            var boxOneCircuit = circuits.FirstOrDefault(circuit => circuit.Contains(boxOne));
            var boxTwoCircuit = circuits.FirstOrDefault(circuit => circuit.Contains(boxTwo));

            if (boxOneCircuit is not null && boxTwoCircuit is not null)
            {
                circuits.Remove(boxTwoCircuit);
                boxOneCircuit.AddRange(boxTwoCircuit);
            }
            else if (boxOneCircuit is null && boxTwoCircuit is null)
                circuits.Add([boxOne, boxTwo]);
            else if (boxOneCircuit is not null && boxTwoCircuit is null)
                boxOneCircuit.Add(boxTwo);
            else if (boxOneCircuit is null && boxTwoCircuit is not null)
                boxTwoCircuit.Add(boxOne);

            if (circuits.Count == 1 && circuits.First().Count == boxes.Length)
                return (boxOne.X * boxTwo.X).ToString();
        }
    }

}