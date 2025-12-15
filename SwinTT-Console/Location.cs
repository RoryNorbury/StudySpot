namespace SwinTT_Console;

public class Location(string name)
{
    readonly List<Entry> entries = new();
    internal string Name { get; } = name;

    void AddEntry(Entry e)
    {
        entries.Add(e);
    }

    internal bool IsFreeAt(TimeOnly time, DayOfWeek day, int teachingWeek)
    {
        foreach (Entry entry in entries)
        {
            //If the entry matches all of these, then it's happening now and the room isn't free.
            if (
                entry.Day == day &&
                entry.TeachingWeeks.Contains(teachingWeek) &&
                entry.StartTime < time &&
                entry.EndTime > time)
            {
                return false;
            }
        }

        //If none of the entries match then the room is free at the specified time.
        return true;
    }

    internal static Location[] SortEntriesByLocation(Entry[] entries)
    {
        List<Location> locations = new();
        foreach (Entry e in entries)
        {
            Location? locationOfEntry = locations.Find(l => l.Name == e.Location);
            if (locationOfEntry is null)
            {
                locationOfEntry = new Location(e.Location);
                locations.Add(locationOfEntry);
            }

            locationOfEntry.AddEntry(e);
        }

        return locations.ToArray();
    }
}
