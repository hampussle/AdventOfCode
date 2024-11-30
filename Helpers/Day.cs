namespace Helpers;

public abstract class Day
{
    public Day(int year, int day)
    {
        if (year < 2015 || year > 2024)
            throw new ArgumentException("Year needs to be between 2015 and 2024");
        if (day < 1 || day > 25)
            throw new ArgumentException("Day needs to be between 1 and 25");
        YearNumber = year;
        DayNumber = day;
    }

    public int DayNumber { get; init; }
    public int YearNumber { get; init; }
    public bool UseTestInput { get; set; }
    public string Input => UseTestInput ? InputHandler.GetTestInput(YearNumber, DayNumber)
                                        : InputHandler.GetInputAsync(YearNumber, DayNumber).Result;
    public string[] SplitInput => [.. Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)];

    public virtual string PartOne() => "no answer";
    public virtual string PartTwo() => "no answer";
}