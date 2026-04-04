using Helpers;
using Microsoft.Z3;

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
        var buttons = SplitInput
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

        var joltages = SplitInput
            .Select(line =>
            {
                var start = line.IndexOf('{');
                var end = line.IndexOf('}');
                return line.Substring(start + 1, end - start - 1)
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            })
            .ToArray();

        long result = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            var goal = joltages[i];
            var buttonOptions = buttons[i];

            using var ctx = new Context();
            using var opt = ctx.MkOptimize();

            var presses = Enumerable.Range(0, buttonOptions.Length)
                .Select(j => ctx.MkIntConst($"p{j}"))
                .ToArray();

            foreach (var press in presses)
                opt.Add(ctx.MkGe(press, ctx.MkInt(0)));

            for (int counter = 0; counter < goal.Length; counter++)
            {
                var affecting = Enumerable.Range(0, buttonOptions.Length)
                    .Where(j => buttonOptions[j].Contains(counter))
                    .Select(j => presses[j])
                    .ToArray();

                if (affecting.Length > 0)
                {
                    ArithExpr sum = affecting.Length == 1 ? affecting[0] : ctx.MkAdd(affecting);
                    opt.Add(ctx.MkEq(sum, ctx.MkInt(goal[counter])));
                }
                else if (goal[counter] > 0)
                    return "0";
            }

            opt.MkMinimize(presses.Length == 1 ? presses[0] : ctx.MkAdd(presses));
            opt.Check();

            var model = opt.Model;
            var machineResult = presses.Sum(p => ((IntNum)model.Evaluate(p, true)).Int64);
            result += machineResult;
        }

        return result.ToString();
    }

}