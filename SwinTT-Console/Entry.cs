namespace SwinTT_Console;

public class Entry
{
    TimeManager.Day day;
    TimeOnly startTime;
    TimeOnly endTime;
    string location;
    int[] teachingWeeks;

    public Entry(string serializedEntry)
    {
        //Format for the entries in the CSV are as follows.
        //This seems to be followed consistantly except that no entries seem to contain the final "Date(s)" field.
        //"Activity Name","Unit","Activity Type Name","Day","Start","End","Activity Duration","Teacher","Location","Teaching Week","Date(s)"

        string[] serializedFields = serializedEntry
            .Split("\",\"") //Split at the commas.
            .Select(field => field.Trim('\"')) //Remove the extraneous quote marks helpfully provided in the file...
            .ToArray(); //Convert back to a usable form.

        if (serializedFields.Length != 10) throw new Exception("Unexpected length of serialized fields array");

        string serializedDay = serializedFields[3];
        string serializedStart = serializedFields[4];
        string serializedEnd = serializedFields[5];
        string serializedLocation = serializedFields[8];
        string serializedTeachingWeeks = serializedFields[9];

        day = TimeManager.DayStringDeserializer(serializedDay);
        startTime = TimeManager.TimeStringDeserializer(serializedStart);
        endTime = TimeManager.TimeStringDeserializer(serializedEnd);
        location = serializedLocation;
        teachingWeeks = TimeManager.TeachingWeekDeserializer(serializedTeachingWeeks);
    }
}
