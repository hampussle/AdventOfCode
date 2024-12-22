using Helpers;
using System.Linq;

namespace Solutions.Year2024;

public class Day5(int year, int day) : Day(year, day)
{

    static IList<IList<int>> Permute(int[] nums)
    {
        var list = new List<IList<int>>();
        return DoPermute(nums, 0, nums.Length - 1, list);
    }

    static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
    {
        if (start == end)
        {
            list.Add(new List<int>(nums));
        }
        else
        {
            for (var i = start; i <= end; i++)
            {
                Swap(ref nums[start], ref nums[i]);
                DoPermute(nums, start + 1, end, list);
                Swap(ref nums[start], ref nums[i]);
            }
        }

        return list;
    }

    static void Swap(ref int a, ref int b)
    {
        var temp = a;
        a = b;
        b = temp;
    }

    (bool isValid, int invalidIdx) IsValidUpdate(int[] update, Dictionary<int, List<int>> pagesBefore, Dictionary<int, List<int>> pagesAfter)
    {
        for (int idx = 0; idx < update.Length; idx++)
        {
            int page = update[idx];

            // check if allowed before other pages
            for (int j = idx + 1; j < update.Length; j++)
            {
                int numberAfter = update[j];
                var isAllowedBefore = IsAllowedBefore(page, numberAfter, pagesBefore, pagesAfter);
                if (!isAllowedBefore)
                {
                    return (false, j);
                }
            }
        }
        return (true, -1);
    }

    Comparer<int> GetComparer(Dictionary<int, List<int>> pagesBefore, Dictionary<int, List<int>> pagesAfter)
    {
        int Compare(int before, int after)
        {
            if (pagesAfter.TryGetValue(before, out var afterList))
            {
                if (afterList.Contains(after))
                {
                    return -1;
                }
            }
            //else if (pagesAfter.TryGetValue(after, out afterList))
            //{
            //    if (afterList.Contains(before))
            //        return 1;
            //}
            return 1;
        }
        return Comparer<int>.Create((a, b) => Compare(a, b));
    }

    int[] FixUpdate(int[] update, Dictionary<int, List<int>> pagesBefore, Dictionary<int, List<int>> pagesAfter)
    {
        List<int> ordered = [];
        List<int> remaining = update.ToList();

        while (remaining.Count > 0)
        {
            for (int i = 0; i < remaining.Count; i++)
            {
                int currNum = remaining[i];
                // no dependencies, just add to ordered
                // delete from remaining
                if (!pagesBefore.ContainsKey(currNum) && !pagesAfter.ContainsKey(currNum))
                {
                    ordered.Add(currNum);
                    remaining.Remove(currNum);
                    continue;
                }
            }
        }

        //Queue<int[]> queue = new();

        //queue.Enqueue(update);


        //while (queue.Count > 0)
        //{
        //    var curr = queue.Dequeue().Select(x => x).ToArray();
        //    var (isValid, invalidIdx) = IsValidUpdate(curr, pagesBefore, pagesAfter);

        //    if (isValid)
        //        return curr;

        //    // try each swap that could be valid
        //    for (int i = 0; i < curr.Length; i++)
        //    {
        //        var copy = curr.Select(x => x).ToArray();
        //        var other = copy[i];
        //        var incorrectNumber = copy[invalidIdx];
        //        copy[i] = incorrectNumber;
        //        copy[invalidIdx] = other;
        //        queue.Enqueue(copy);
        //    }
        //}

        //var allPossiblePermuations = Permute(update);
        //foreach (var possible in allPossiblePermuations)
        //{
        //    var arr = possible.ToArray();
        //    if (IsValidUpdate(arr, pagesBefore, pagesAfter).isValid)
        //        return arr;
        //}

        return ordered.ToArray();

        //while (true)
        //{
        //    var (isValid, invalidIdx) = IsValidUpdate(update, pagesBefore, pagesAfter);
        //    if (isValid)
        //        return update;

        //    for (int i = 0; i < update.Length; i++)
        //    {
        //        if (invalidIdx == i)
        //            continue;

        //        // swap values
        //        (update[i], update[invalidIdx]) = (update[invalidIdx], update[i]);
        //    }
        //}
    }

    bool IsAllowedBefore(int before, int after, Dictionary<int, List<int>> pagesBefore, Dictionary<int, List<int>> pagesAfter)
    {
        var allowedBefore = pagesBefore.TryGetValue(before, out var listAfter) ? listAfter : [];
        return !allowedBefore.Contains(after);
    }

    public override string PartOne()
    {
        // (75)|10   (75)|20    10, 20
        Dictionary<int, List<int>> pagesAfter = new();

        // 40|(75)  30|(75)     40, 30
        Dictionary<int, List<int>> pagesBefore = new();

        List<int[]> updates = [];

        string input = Input.Replace("\r\n", "\n");
        string[] splitInput = input.Split("\n");

        // parsing input
        foreach (string line in splitInput)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Contains('|'))
            {
                var split = line.Split('|');
                int before = int.Parse(split[0]);
                int after = int.Parse(split[1]);

                if (pagesAfter.TryGetValue(before, out var pagesAfterList))
                {
                    pagesAfterList.Add(after);
                }
                else
                {
                    pagesAfter.Add(before, [after]);
                }

                if (pagesBefore.TryGetValue(after, out var pagesBeforeList))
                {
                    pagesBeforeList.Add(before);
                }
                else
                {
                    pagesBefore.Add(after, [before]);
                }
            }

            else if (line.Contains(','))
            {
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
            }
        }

        int total = 0;

        foreach (var update in updates)
        {
            bool isValid = true;

            List<int> previous = [];


            for (int idx = 0; idx < update.Length; idx++)
            {
                int page = update[idx];

                // check if allowed before other pages
                var allowedAfter = pagesAfter.TryGetValue(page, out var listAfter) ? listAfter : [];
                for (int j = idx + 1; j < update.Length; j++)
                {
                    int numberAfter = update[j];
                    var isAllowedBefore = IsAllowedBefore(page, numberAfter, pagesBefore, pagesAfter);
                    if (!isAllowedBefore)
                    {
                        isValid = false;
                    }
                }

                // check if allowed after other pages - NO NEED


                //var allowedBefore = pagesBefore.TryGetValue(page, out var listBefore) ? listBefore : [];


            }

            if (isValid)
            {
                total += update[update.Length / 2];
            }
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        // (75)|10   (75)|20    10, 20
        Dictionary<int, List<int>> pagesAfter = new();

        // 40|(75)  30|(75)     40, 30
        Dictionary<int, List<int>> pagesBefore = new();

        List<int[]> updates = [];

        string input = Input.Replace("\r\n", "\n");
        string[] splitInput = input.Split("\n");

        // parsing input
        foreach (string line in splitInput)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.Contains('|'))
            {
                var split = line.Split('|');
                int before = int.Parse(split[0]);
                int after = int.Parse(split[1]);

                if (pagesAfter.TryGetValue(before, out var pagesAfterList))
                {
                    pagesAfterList.Add(after);
                }
                else
                {
                    pagesAfter.Add(before, [after]);
                }

                if (pagesBefore.TryGetValue(after, out var pagesBeforeList))
                {
                    pagesBeforeList.Add(before);
                }
                else
                {
                    pagesBefore.Add(after, [before]);
                }
            }

            else if (line.Contains(','))
            {
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
            }
        }

        int total = 0;

        List<int[]> incorrectUpdates = [];

        foreach (var update in updates)
        {
            bool isValid = true;

            for (int idx = 0; idx < update.Length; idx++)
            {
                if (!isValid)
                    break;

                int page = update[idx];

                // check if allowed before other pages
                for (int j = idx + 1; j < update.Length; j++)
                {
                    int numberAfter = update[j];
                    var isAllowedBefore = IsAllowedBefore(page, numberAfter, pagesBefore, pagesAfter);
                    if (!isAllowedBefore)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (!isValid)
            {
                incorrectUpdates.Add(update);
            }
        }

        // Correct updates
        List<int[]> fixedUpdates = [];
        var comparer = GetComparer(pagesBefore, pagesAfter);
        foreach (var update in incorrectUpdates)
        {
            //var fixedUpdate = FixUpdate(update, pagesBefore, pagesAfter);

            var fixedUpdate = update.OrderBy(p => p, comparer).ToArray();

            fixedUpdates.Add(fixedUpdate);

            //for (int idx = 0; idx < update.Length; idx++)
            //{
            //    int page = update[idx];

            //    // check if allowed before other pages
            //    for (int j = idx + 1; j < update.Length; j++)
            //    {
            //        int numberAfter = update[j];
            //        var isAllowedBefore = IsAllowedBefore(page, numberAfter, pagesBefore, pagesAfter);
            //        if (!isAllowedBefore)
            //        {
            //            // incorrect, try fix it
            //            IsValidUpdate();
            //        }
            //    }
            //}
        }

        // Add middle number for each fixed update
        foreach (var update in fixedUpdates)
        {
            total += update[update.Length / 2];
        }

        return total.ToString();
    }

}