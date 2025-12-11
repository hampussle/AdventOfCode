using Helpers;

namespace Solutions.Year2025;

public class Day11(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        var inputDevices = SplitInput.Select(line => line.RemoveFromString([':']).Split(' '));
        var devices = new Dictionary<string, string[]>();
        var stack = new Stack<string>();
        
        foreach (var device in inputDevices)
            devices.Add(device[0], device[1..]);

        foreach (var device in devices["you"])
            stack.Push(device);

        int counter = 0;
        while (stack.Count != 0)
        {
            var curr = stack.Pop();
            if (curr == "out")
            {
                counter++;
                continue;
            }
            foreach (var output in devices[curr])
                stack.Push(output);
        }

        return counter.ToString();
    }

    public override string PartTwo()
    {
        var inputDevices = SplitInput.Select(line => line.RemoveFromString([':']).Split(' '));
        var devices = new Dictionary<string, string[]>();
        var stateTracker = new Dictionary<(string device, bool hitfft, bool hitdac), long>();

        foreach (var device in inputDevices)
        {
            devices.Add(device[0], device[1..]);
            stateTracker.Add((device[0], false, false), 0);
            stateTracker.Add((device[0], true, false), 0);
            stateTracker.Add((device[0], false, true), 0);
            stateTracker.Add((device[0], true, true), 0);
        }

        stateTracker.Add(("out", false, false), 0);
        stateTracker.Add(("out", true, false), 0);
        stateTracker.Add(("out", false, true), 0);
        stateTracker.Add(("out", true, true), 0);
        stateTracker[("svr", false, false)] = 1;

        while (stateTracker.Any(path => path.Value != 0 && path.Key.device != "out"))
        {
            foreach (var kvp in stateTracker)
            {
                if (kvp.Value == 0)
                    continue;
                var (device, hasHitfft, hasHitdac) = kvp.Key;
                bool hitfft = hasHitfft;
                bool hitdac = hasHitdac;
                if (device == "out")
                    continue;
                if (device == "fft")
                    hitfft = true;
                if (device == "dac")
                    hitdac = true;
                foreach (var output in devices[device])
                    stateTracker[(output, hitfft, hitdac)] += stateTracker[(device, hasHitfft, hasHitdac)];
                stateTracker[(device, hasHitfft, hasHitdac)] = 0;
            }
        }

        return stateTracker[("out", true, true)].ToString();
    }

}