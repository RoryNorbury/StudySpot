namespace SwinTT_Console;

public static class TimeManager
{
    internal static Day DayStringDeserializer(string serializedDay)
    {
        return serializedDay switch
        {
            "Mon" => Day.Monday,
            "Tue" => Day.Tuesday,
            "Wed" => Day.Wednesday,
            "Thu" => Day.Thursday,
            "Fri" => Day.Friday,
            "Sat" => Day.Saturday,
            "Sun" => Day.Sunday,
            _ => throw new Exception("Not a valid day serialization")
        };
    }

    internal static TimeOnly TimeStringDeserializer(string serializedTime)
    {
        //Separate the hours (which could be 1 or 2 digits) from the constant-length rest of the string
        string[] split = serializedTime.Split(':');

        //The first part of the split will be the number of hours for the TimeOnly
        int hours = int.Parse(split[0]);

        //The second part of the split contains the minutes (2 chars) and the am/pm indicator (2 chars).
        string minutesString = string.Concat(split[1][0], split[1][1]);
        int minutes = int.Parse(minutesString);

        //Add on 12 if we are looking at PM, or skip for AM.
        if (split[1][2] == 'p') hours += 12;

        //Normalize to 0-23 instead of 1-24. (12 am and pm is the wrong way around if you think about it lol).
        if (hours is 12 or 24) hours -= 12;

        return new TimeOnly(hours, minutes);
    }

    internal static int[] TeachingWeekDeserializer(string serializedTeachingWeeks)
    {
        return [0];
    }

    internal enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
