using Helpers;

namespace Solutions.Year2025;

public class Day10(int year, int day) : Day(year, day)
{

    public override string PartOne()
    {
        var indicators = SplitInput
            .Select(line =>
                line
                .Skip(1)
                .TakeWhile(c => c != ']')
                .Select(c => c == '#')
                .ToArray())
            .ToArray();

        var wiring = SplitInput
            .Select(line =>
                line
                .Split('(', ')')
                .Where(w => !string.IsNullOrWhiteSpace(w) && w.All(c => char.IsNumber(c) || c == ','))
                .Select(w =>
                    w.Split(',')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray())
            .ToArray();

        long result = 0;
        for (int i = 0; i < indicators.Length; i++)
        {
            var goal = indicators[i];
            var start = new bool[goal.Length]; 
            var buttons = wiring[i];
            Queue<(int[], int, bool[], List<int[]>)> buttonPresses = new();
            foreach (int[] button in buttons)
            buttonPresses.Enqueue((button, 0, start, []));
            while (buttonPresses.TryDequeue(out var buttonPress))
            {
                var (button, tryCount, state, pressed) = buttonPress;
                if (pressed.Contains(button))
                    continue;
                var nextState = PressButton(button, state);
                tryCount += 1;
                if (nextState.SequenceEqual(goal))
                {
                    Console.WriteLine($"{tryCount} steps");
                    result += tryCount;
                    break;
                }
                pressed = [.. pressed];
                pressed.Add(button);
                foreach (int[] b in buttons)
                    buttonPresses.Enqueue((b, tryCount, nextState, pressed));
            }

            static bool[] PressButton(int[] button, bool[] indicator) => [.. indicator.Select((b, i) => button.Contains(i) ? !b : b)];
        }

        return result.ToString();
    }

    public override string PartTwo()
    {
        var wiring = SplitInput
            .Select(line =>
                line
                .Split('(', ')')
                .Where(w => !string.IsNullOrWhiteSpace(w) && w.All(c => char.IsNumber(c) || c == ','))
                .Select(w =>
                    w.Split(',')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray())
            .ToArray();

        var joltage = SplitInput
            .Select(line =>
                line
                .SkipWhile(c => c != '{')
                .Skip(1)
                .TakeWhile(c => c != '}')
                .ConcatChars()
                .Split(',')
                .Select(int.Parse)
                .ToArray())
            .ToArray();

        long result = 0;
        for (int i = 0; i < joltage.Length; i++)
        {
            var goal = joltage[i];
            var start = new int[goal.Length];
            var buttons = wiring[i];
            Queue<(int[], int, int[])> buttonPresses = new();
            foreach (int[] button in buttons)
                buttonPresses.Enqueue((button, 0, start));
            while (buttonPresses.TryDequeue(out var buttonPress))
            {
                var (button, tryCount, state) = buttonPress;
                var nextState = PressButton(button, state);
                if (!ValidState(goal, nextState))
                    continue;
                tryCount += 1;
                if (nextState.SequenceEqual(goal))
                {
                    Console.WriteLine($"{tryCount} steps");
                    result += tryCount;
                    break;
                }
                foreach (int[] b in buttons)
                    buttonPresses.Enqueue((b, tryCount, nextState));
            }

            static int[] PressButton(int[] button, int[] joltage) => [.. joltage.Select((b, i) => button.Contains(i) ? b + 1 : b)];
        }

        return result.ToString();

        static bool ValidState(int[] goal, int[] nextState)
        {
            for (int j = 0; j < nextState.Length; j++)
                if (nextState[j] > goal[j])
                    return false;
            return true;
        }
    }

}