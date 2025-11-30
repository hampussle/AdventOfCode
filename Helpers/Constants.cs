namespace Helpers;

public static class Constants
{
    public const int MinYear = 2015;
    public const int MaxYear = 2025;
    public const int MinDay = 1;
    public const int MaxDay = 25;

    public static bool ValidYear(int year) => year >= MinYear && year <= MaxYear;
    public static bool ValidDay(int day) => day >= MinDay && day <= MaxDay;
    public static bool ValidDay(int year, int day) => ValidYear(year) && ValidDay(day);
}
