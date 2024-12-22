using Helpers;

namespace Solutions.Year2024;

public class Day7(int year, int day) : Day(year, day)
{
    public int IsCorrectOperations(int[] parts, long result, bool[] operations)
    {
        int total = parts[0];
        for (int i = 0; i < parts.Length - 1; i++)
        {
            int secondPart = parts[i + 1];
            total = operations[i] ? total + secondPart : total * secondPart;
        }
        if (total == result)
            return 0;
        else if (total < result)
            return -1;
        else
            return 1;
    }

    public List<List<bool>> GetPossiblePermutations(int length)
    {
        List<List<bool>> bools = [[true], [false]];
        for (int i = 1; i < length; i++)
        {
            foreach (var bls in bools)
            {
                bls.Add(true);
            }
        }
        return bools;
    }

    public Operations AddAddition(Operations operations, HashSet<Operations> visited)
    {
        Operations newOperations = new(operations._operations.ToArray());
        for (int i = 0; i < operations._operations.Length; i++)
        {
            bool curr = operations._operations[i];
            if (!curr)
            {
                newOperations._operations[i] = true;
                if (visited.Contains(newOperations))
                {
                    newOperations._operations[i] = false;
                }
                else
                {
                    break;
                }
            }
        }

        return newOperations;
    }

    public Operations AddMultiplication(Operations operations, HashSet<Operations> visited)
    {
        Operations newOperations = new(operations._operations.ToArray());
        for (int i = 0; i < operations._operations.Length; i++)
        {
            bool curr = operations._operations[i];
            if (curr)
            {
                newOperations._operations[i] = false;
                if (visited.Contains(newOperations))
                {
                    newOperations._operations[i] = true;
                }
                else
                {
                    break;
                }
            }
        }

        return newOperations;
    }

    public class Operations(bool[] operations) : IEquatable<Operations>
    {
        public bool[] _operations = operations;

        public bool Equals(Operations? other)
        {
            if (other is null)
                return false;
            if (_operations.Length != other._operations.Length)
                return false;
            for (int i = 0; i < _operations.Length; i++)
            {
                if (_operations[i] != other._operations[i])
                    return false;
            }
            return true;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Operations);
        }

        public override int GetHashCode()
        {
            return _operations.GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(_operations.Select(x => x.ToString()));
        }
    }

    public static bool IsValid(long target, List<long> parts)
    {
        if (parts.Count == 1)
            return target == parts[0];

        List<long> multParts = [parts[0] * parts[1]];
        multParts.AddRange(parts.Skip(2));

        List<long> addParts = [parts[0] + parts[1]];
        addParts.AddRange(parts.Skip(2));

        List<long> concatParts = [long.Parse(parts[0].ToString() + parts[1].ToString())];
        concatParts.AddRange(parts.Skip(2));

        if (IsValid(target, multParts))
            return true;
        if (IsValid(target, addParts))
            return true;
        if (IsValid(target, concatParts))
            return true;

        return false;
    }

    public override string PartOne()
    {
        long total = 0;
        foreach (string line in Input.Replace("\r", string.Empty).Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            //190: 10 19
            var split = line.Split(':');
            long result = long.Parse(split[0]);
            List<long> parts = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            if (IsValid(result, parts))
            {
                Console.WriteLine($"{result} VALID");
                total += result;
            }
            else
            {
                Console.WriteLine($"{result} INVALID");
            }
        }
        return total.ToString();
        //    Queue<Operations> queue = new();
        //    HashSet<Operations> visited = [];
        //    Operations? operations = new(new bool[parts.Length - 1]);
        //    Operations allTrueOperations = new(operations._operations.Select(_ => true).ToArray());
        //    queue.Enqueue(operations);
        //    queue.Enqueue(allTrueOperations);

        //    while (queue.TryDequeue(out operations))
        //    {
        //        if (visited.Contains(operations))
        //        {
        //            string partsStr = string.Concat(parts.Select(x => x.ToString() + ", "));
        //            Console.WriteLine($"parts {partsStr} found to be invalid");
        //            break;
        //        }
        //        visited.Add(operations);

        //        var eq = IsCorrectOperations(parts, result, operations._operations);

        //        if (eq == 0)
        //        {
        //            total += result;
        //            Console.WriteLine($"operation {operations} valid for {parts}");
        //            break;
        //        }
        //        else if (eq == -1)
        //        {
        //            Operations newOperations = AddMultiplication(operations, visited);
        //            queue.Enqueue(newOperations);
        //        }
        //        else
        //        {
        //            Operations newOperations = AddAddition(operations, visited);
        //            queue.Enqueue(newOperations);
        //        }


        //    }
        //}
    }

    public override string PartTwo()
    {
        return base.PartTwo();
    }

}