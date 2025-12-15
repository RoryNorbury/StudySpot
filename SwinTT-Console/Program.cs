using SwinTT_Console;

List<Entry> entries = new();
for (int i = 1; i <= YearlyUpdateSettings.NumberOfCsvFiles; i++)
{
    string filePath = $"../../../../CsvFiles/{i}.csv";
    Entry[] newEntries = FileLoader.Load(filePath);
    entries.AddRange(newEntries);
}

Location[] locations = Location.SortEntriesByLocation(entries.ToArray());

DateTime currentDateTime = DateTime.Now;
int currentTeachingWeek = TimeManager.GetTeachingWeek(currentDateTime);
DayOfWeek currentDayOfWeek = currentDateTime.DayOfWeek;
TimeOnly currentTime = TimeOnly.FromDateTime(currentDateTime);

List<Location> locationsCurrentlyFree = new();
foreach (Location l in locations)
{
    if (l.IsFreeAt(currentTime, currentDayOfWeek, currentTeachingWeek)) locationsCurrentlyFree.Add(l);
    Console.WriteLine(l.Name);
}
