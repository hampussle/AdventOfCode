using Helpers;
using System.Drawing;

namespace Solutions.Year2024;

public class Day13(int year, int day) : Day(year, day)
{
    /*
Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176
     */
    class ClawMachine
    {
        public Point ButtonA;
        public Point ButtonB;
        public Point Prize;
    }

    List<ClawMachine> GetClawMachines()
    {
        List<ClawMachine> clawMachines = [];
        ClawMachine curr = new();
        foreach (string line in Input.RemoveFromString("\r").Split('\n'))
        {
            if (line.Contains("Button A"))
            {
                int[] parts = line.RemoveFromString("Button A: X+", " Y+").Split(',').Select(int.Parse).ToArray();
                curr.ButtonA = new(parts[0], parts[1]);
            }
            else if (line.Contains("Button B"))
            {
                int[] parts = line.RemoveFromString("Button B: X+", " Y+").Split(',').Select(int.Parse).ToArray();
                curr.ButtonB = new(parts[0], parts[1]);
            }
            else if (line.Contains("Prize"))
            {
                int[] parts = line.RemoveFromString("Prize: X=", " Y=").Split(',').Select(int.Parse).ToArray();
                curr.Prize = new(parts[0], parts[1]);
            }
            else
            {
                ClawMachine clawMachine = new()
                {
                    ButtonA = curr.ButtonA,
                    ButtonB = curr.ButtonB,
                    Prize = curr.Prize,
                };
                clawMachines.Add(clawMachine);
                curr = new();
            }
        }
        return clawMachines;
    }

    public override string PartOne()
    {
        // https://www.elevri.com/sv/kurser/linjar-algebra/determinanter
        // https://www.cuemath.com/algebra/cramers-rule/

        // Ax = A button press value for X
        // Bx = B button press value for X
        // Ay = A button press value for Y
        // By = B button press value for Y
        // A = amount of A button presses
        // B = amount of B button presses
        // (Ax * A) + (Bx * B) = prizeX
        // (Ay * A) + (By * B) = prizeY

        // cramers rule
        // A = (prizeX * By - prizeY * Bx) / (Ax * By - Ay * Bx)
        // B = (Ax * prizeY - Ay * prizeX) / (Ax * By - Ay * Bx)

        var clawMachines = GetClawMachines();

        int total = 0;
        foreach (var clawMachine in clawMachines)
        {
            int d = clawMachine.ButtonA.X * clawMachine.ButtonB.Y - clawMachine.ButtonA.Y * clawMachine.ButtonB.X;
            int da = clawMachine.Prize.X * clawMachine.ButtonB.Y - clawMachine.Prize.Y * clawMachine.ButtonB.X;
            int db = clawMachine.ButtonA.X * clawMachine.Prize.Y - clawMachine.ButtonA.Y * clawMachine.Prize.X;

            if (da % d != 0 || db % d != 0)
            {
                continue;
            }

            int a = da / d;
            int b = db / d;

            total += a * 3;
            total += b;

            Console.WriteLine($"A: {a}, B: {b}");
        }

        return total.ToString();
    }

    public override string PartTwo()
    {
        var clawMachines = GetClawMachines();

        long total = 0;
        foreach (var clawMachine in clawMachines)
        {
            long prizeX = clawMachine.Prize.X + 10000000000000;
            long prizeY = clawMachine.Prize.Y + 10000000000000;

            int d = clawMachine.ButtonA.X * clawMachine.ButtonB.Y - clawMachine.ButtonA.Y * clawMachine.ButtonB.X;
            long da = prizeX * clawMachine.ButtonB.Y - prizeY * clawMachine.ButtonB.X;
            long db = clawMachine.ButtonA.X * prizeY - clawMachine.ButtonA.Y * prizeX;

            if (da % d != 0 || db % d != 0)
            {
                continue;
            }

            long a = da / d;
            long b = db / d;

            total += a * 3;
            total += b;

            Console.WriteLine($"A: {a}, B: {b}");
        }

        return total.ToString();
    }

}