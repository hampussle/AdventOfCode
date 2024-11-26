namespace Helpers;

public abstract class Day(int day)
{
    private readonly string _input = InputHandler.GetInputAsync(day).Result;
    private string TestInput => InputHandler.GetTestInput(DayNumber);
    public string Input => UseTestInput ? TestInput : _input;
    public string[] SplitInput => Input.Split(Environment.NewLine).SkipLast(1).ToArray();
    public bool UseTestInput { get; set; }
    private readonly int DayNumber = day;

    public virtual string PartOne() => "no answer";
    public virtual string PartTwo() => "no answer";
}