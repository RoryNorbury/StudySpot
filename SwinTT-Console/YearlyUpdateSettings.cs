namespace SwinTT_Console;

public static class YearlyUpdateSettings
{
    internal const int NumberOfCsvFiles = 10;

    internal static int GetTeachingWeek(DateTime dateTime)
    {
        //For 2026, Teaching Week 1 begins on the 29th of Dec 2025, and they continue up to 65 from there.
        //That means Teaching Week 2 begins on the 5th of Jan 2026, and the hardcoded calulcation below
        //should give accurate results until the end of 2026. This calculation will need to be adjusted
        //a new timetable is released (yearly). Unfortunately for now I do not see how to do this automatically.
        int dayOfYear = dateTime.DayOfYear;
        return (dayOfYear + 2) / 7 + 1;
    }
}
