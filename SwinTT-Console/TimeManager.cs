namespace SwinTT_Console;

public static class TimeManager
{
    internal static DayOfWeek DeserializeDay(string serializedDay)
    {
        return serializedDay switch
        {
            "Mon" => DayOfWeek.Monday,
            "Tue" => DayOfWeek.Tuesday,
            "Wed" => DayOfWeek.Wednesday,
            "Thu" => DayOfWeek.Thursday,
            "Fri" => DayOfWeek.Friday,
            "Sat" => DayOfWeek.Saturday,
            "Sun" => DayOfWeek.Sunday,
            _ => throw new Exception("Not a valid day serialization")
        };
    }

    internal static TimeOnly DeserializeTime(string serializedTime)
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

    internal static int[] DeserializeTeachingWeek(string serializedTeachingWeeks)
    {
        //Teaching weeks are stored as ranges (XX-XX), possibly more than one range separated by a comma.
        string[] ranges = serializedTeachingWeeks.Split(',');
        List<int> teachingWeeks = new();
        foreach (string range in ranges)
        {
            //Extract the first and last weeks in the range (stored in the string).
            //There are two types of hyphen characters used in the range, u+002D and U+2011. Lol surely pick one.
            //We also need to trim out random Zero Width Space characters (U+200B) sprinkled into the lovely data.
            string[] startAndEndOfRange = range.Split('‑', '-');
            int startOfRange = int.Parse(startAndEndOfRange[0].Trim('​'));
            int endOfRange;
            try
            {
                endOfRange = int.Parse(startAndEndOfRange[1].Trim('​'));
            }
            catch (IndexOutOfRangeException)
            {
                //This will happen if there is no range, just a single week.
                endOfRange = startOfRange;
            }

            //Convert to a list of weeks that are bounded by the range.
            for (int i = startOfRange; i <= endOfRange; i++)
            {
                teachingWeeks.Add(i);
            }
        }

        return teachingWeeks.ToArray();
    }

    internal static int GetTeachingWeek(DateTime dateTime)
    {
        //This function is here because it makes more sense from a code organisation standpoint. However,
        //it simply calls the function in the "yearly update settings" so that eveyrthing that needs to be
        //changed yearly can be done in one place. Ideally, that function in yearly update settings should
        //only ever be called here, and other parts of their code should make their calls to this function.
        return YearlyUpdateSettings.GetTeachingWeek(dateTime);
    }
}
