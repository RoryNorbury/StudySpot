namespace StudySpot.Backend;

public class Entry
{
    internal readonly DayOfWeek Day;
    internal readonly TimeOnly EndTime;
    internal readonly string Location;
    internal readonly TimeOnly StartTime;
    internal readonly int[] TeachingWeeks;

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

        Day = TimeManager.DeserializeDay(serializedDay);
        StartTime = TimeManager.DeserializeTime(serializedStart);
        EndTime = TimeManager.DeserializeTime(serializedEnd);
        Location = serializedLocation;
        TeachingWeeks = TimeManager.DeserializeTeachingWeek(serializedTeachingWeeks);
    }
}
