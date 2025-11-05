namespace Helpers;

public abstract class Day
{
    public Day(int year, int day)
    {
        if (!Constants.ValidYear(year))
            throw new ArgumentException($"Year needs to be between {Constants.MinYear} and {Constants.MaxYear}");
        if (!Constants.ValidDay(day))
            throw new ArgumentException("Day needs to be between 1 and 25");
        YearNumber = year;
        DayNumber = day;
    }

    public int DayNumber { get; init; }
    public int YearNumber { get; init; }
    public bool UseTestInput { get; set; }
    public string Input => UseTestInput ? InputHandler.GetTestInput(YearNumber, DayNumber)
                                        : InputHandler.GetInputAsync(YearNumber, DayNumber).Result;
    public string[] SplitInput => [.. Input.ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)];

    public virtual string PartOne() => "no answer";
    public virtual string PartTwo() => "no answer";
}