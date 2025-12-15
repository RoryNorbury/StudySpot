namespace StudySpot;

public static class FileLoader
{
    public static Entry[] Load(string filePath)
    {
        //Files begin with 5 lines of heading befor the actual data.
        const int linesOfHeadingInFile = 5;

        //Using statement will automatically close the reader even if there is an exception (creates a try/finally block behind the scenes).
        using StreamReader reader = new(filePath);

        //Skip any header lines in the file.
        for (int i = 0; i < linesOfHeadingInFile; i++) reader.ReadLine();

        List<Entry> entries = new();

        //Assigns the read line to the line variable but ends the loop if the line is null.
        while (reader.ReadLine() is { } line)
        {
            entries.Add(new Entry(line));
        }

        return entries.ToArray();
    }
}
