using Helpers;

namespace Solutions.Year2024;

public class Day9(int year, int day) : Day(year, day)
{
    public override string PartOne()
    {
        bool isFile = true;
        int id = 0;

        List<int?> curr = [];

        List<int> space = [];

        for (int idx = 0; idx < Input.Length; idx++)
        {
            char c = Input[idx];
            double number = char.GetNumericValue(c);
            if (isFile)
            {
                for (int j = 0; j < number; j++)
                {
                    curr.Add(id);
                }
                id++;
            }
            else
            {
                for (int j = 0; j < number; j++)
                {
                    curr.Add(null);
                    space.Add(curr.Count - 1);
                }
            }
            isFile = !isFile;
        }

        while (!IsFinished(curr))
        {
            List<int?> newCurr = new(curr);
            for (int revIdx = curr.Count - 1; revIdx >= 0; revIdx--)
            {
                if (IsFinished(newCurr))
                    break;

                int? revC = curr[revIdx];
                if (revC is not null)
                {
                    int firstSpace = space.Min();
                    space.Remove(firstSpace);
                    newCurr[firstSpace] = revC;
                    newCurr[revIdx] = null;
                }
            }
            curr = newCurr;
        }

        long total = 0;
        for (int idx = 0; idx < curr.Count; idx++)
        {
            int? c = curr[idx];
            if (c is not null)
            {
                total += (long)c * idx;
            }
        }

        return total.ToString();
    }

    static bool IsFinished(List<int?> curr)
    {
        bool foundNumber = false;
        List<int?> currRev = new(curr);
        currRev.Reverse();
        foreach (int? val in currRev)
        {
            if (val is not null)
                foundNumber = true;
            if (val is null && foundNumber)
                return false;
        }

        return true;
    }

    public override string PartTwo()
    {
        bool isFile = true;
        int id = 0;

        List<int?> curr = [];

        List<int> space = [];

        for (int idx = 0; idx < Input.Length; idx++)
        {
            char c = Input[idx];
            double number = char.GetNumericValue(c);
            if (isFile)
            {
                for (int j = 0; j < number; j++)
                {
                    curr.Add(id);
                }
                id++;
            }
            else
            {
                for (int j = 0; j < number; j++)
                {
                    curr.Add(null);
                    space.Add(curr.Count - 1);
                }
            }
            isFile = !isFile;
        }

        List<int?> newCurr = new(curr);

        List<List<int>> spaces2 = [];
        foreach (int slot in space)
        {
            var availableSpaceList = spaces2.FirstOrDefault(list => list.Any(x => x == slot - 1 || x == slot + 1));
            if (availableSpaceList is null)
            {
                spaces2.Add([slot]);
            }
            else
            {
                availableSpaceList.Add(slot);
            }
        }

        HashSet<int> visited = new();

        for (int revIdx = curr.Count - 1; revIdx >= 0; revIdx--)
        {
            int? revC = curr[revIdx];
            if (revC is null)
                continue;

            if (visited.Contains(revC.Value))
                continue;

            List<(int value, int idx)> block = GetLastFullBlock(curr, revIdx);

            if (block.Count == 0)
            {
                continue;
            }

            if (spaces2.FirstOrDefault(list => list.Count >= block.Count && list.All(x => x < (revIdx - block.Count + 1))) is List<int> blockSpaces)
            {
                int firstSpace = blockSpaces.Min();

                //var spacesMin = spaces2.MinBy(x => x.Min())!;
                //if (spacesMin.Min() + spacesMin.Count > revIdx - )
                //    break;

                Console.WriteLine($"Moved block {revC}.");
                visited.Add(revC.Value);

                foreach (var (value, index) in block)
                {
                    //if (firstSpace > revIdx)
                    //{
                    //    Console.WriteLine($"Break on block {revC}.");
                    //    break;
                    //}

                    newCurr[firstSpace] = value;
                    newCurr[index] = null;
                    blockSpaces.Remove(firstSpace);

                    var availableSpaceList = spaces2.FirstOrDefault(list => list.Any(x => x == index - 1 || x == index + 1));
                    if (availableSpaceList is null)
                    {
                        spaces2.Add([index]);
                    }
                    else
                    {
                        availableSpaceList.Add(index);
                    }
                    //blockSpaces.Add(revIdx);

                    //revIdx--;
                    firstSpace++;

                    if (blockSpaces.Count == 0)
                        spaces2.Remove(blockSpaces);
                }

                //PrintLine(newCurr);
            }
            else
            {
                Console.WriteLine($"Block {revC} can't be moved.");
                //revIdx -= block.Count - 2;
            }
        }
        //if (curr.SequenceEqual(newCurr))
        //    break;
        //PrintLine(newCurr);

        Console.WriteLine($"Before length: {curr.Count}");
        Console.WriteLine($"Before Ids: {curr.Count(x => x is not null)}");
        Console.WriteLine($"After length: {newCurr.Count}");
        Console.WriteLine($"After Ids: {newCurr.Count(x => x is not null)}");

        curr = newCurr;
        //}

        long total = 0;
        for (int idx = 0; idx < curr.Count; idx++)
        {
            int? c = curr[idx];
            if (c is not null)
            {
                total += (long)c * idx;
            }
        }

        return total.ToString();
    }

    void PrintLine(List<int?> curr)
    {
        foreach (var item in curr)
        {
            if (item is null)
                Console.Write('.');
            else
                Console.Write(item.ToString());
        }
        Console.WriteLine();
    }

    List<(int value, int index)> GetLastFullBlock(List<int?> curr, int idx)
    {
        if (curr[idx] is not int val)
            return [];

        List<(int value, int index)> block = [(val, idx)];

        for (int i = 1; i < 9; i++)
        {
            int next = idx + i;
            if (next > curr.Count - 1)
                break;

            if (curr[next] is int nextval && nextval == val)
            {
                block.Add((val, next));
            }
            else
            {
                break;
            }
        }

        for (int i = 1; i < 100; i++)
        {
            int next = idx - i;
            if (next < 0)
                break;

            if (curr[next] is int nextval && nextval == val)
            {
                block.Add((val, next));
            }
            else
            {
                break;
            }
        }

        return block.OrderBy(x => x.index).ToList();
    }

}