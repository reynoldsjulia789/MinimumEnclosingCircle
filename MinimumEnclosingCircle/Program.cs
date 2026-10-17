using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;

namespace MinimumEnclosingCircle;

public class Program
{
    private const string graphDirectory = "../Graphs";

    private static class Runtimes
    {
        public static List<double> ReadFileTimes = [];
        public static List<double> FindCircleTimes = [];
    }

    public static void Main()
    {
        Stopwatch stopwatch = new();

        PrintRunInstructions();

        // read points
        string[] filepaths = Directory.GetFiles(graphDirectory);

        List<List<Vector2>> filecontents = GetFileContents(filepaths);

        // find min enclosing circle
        List<Circle?> results = [];

        Console.WriteLine("\r\nAttempting to find minimum enclosing circle for each file.");

        foreach (var points in filecontents)
        {
            stopwatch.Restart();

            results.Add(Welzl.GetMinimumEnclosingCircle(points));

            stopwatch.Stop();
            Runtimes.FindCircleTimes.Add(stopwatch.Elapsed.TotalNanoseconds);
        }

        // print results
        Console.WriteLine();
        Console.WriteLine("Results:");

        for (int idx = 0; idx < results.Count; idx++)
        {
            Console.WriteLine();

            Console.WriteLine("---");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"File: {Path.GetFileName(filepaths[idx])}");
            Console.ResetColor();

            if (results[idx] is null)
            {
                Console.WriteLine("No circle found");
                continue;
            }

            Console.WriteLine("\r\nRuntimes:");
            Console.WriteLine($"\tFile Read Time   - {Runtimes.ReadFileTimes[idx]} ns");
            Console.WriteLine($"\tFind Circle Time - {Runtimes.FindCircleTimes[idx]} ns");

            Console.WriteLine("\r\nCircle:");
            Console.WriteLine($"\tCenter: ({results[idx]!.Center.X}, {results[idx]!.Center.Y})");
            Console.WriteLine($"\tRadius: {results[idx]!.Radius}");
        }
    }

    /// <summary>
    /// Prints instructions on how to run the program
    /// </summary>
    private static void PrintRunInstructions()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine();
        Console.WriteLine("To run:");
        Console.WriteLine($"1) place .txt files containing coordinates into the directory {graphDirectory}");
        Console.WriteLine($"    * the coordinates for each point must be on a separate line");
        Console.WriteLine($"    * the coordinates for each point must be in the following format:");
        Console.WriteLine($"        * floatx, floaty");
        Console.WriteLine($"2) if using the cli, navigate to the directory containing the .csproj file " +
            $"and run the following command:");
        Console.WriteLine($"    * dotnet run");

        Console.ResetColor();
    }

    /// <summary>
    /// Reads X,Y coordinates from the specified files and returns a list containing, for each file that
    /// yielded at least one valid coordinate, a set of unique Vector2 values.
    /// Lines that do not contain exactly two comma-separated values or that cannot be parsed as
    /// floats are ignored. Files that produce no valid coordinates are skipped. 
    /// Duplicate coordinates within a file are removed.
    /// </summary>
    /// <param name="filepaths">Array of files to read</param>
    /// <returns>
    /// list of coordinates per file read, or an empty list if there were no files with usable coordinates.
    /// </returns>
    private static List<List<Vector2>> GetFileContents(string[] filepaths)
    {
        Stopwatch stopwatch = new();

        if (filepaths.Length == 0)
        {
            Console.WriteLine($"No files found in directory ({graphDirectory})");
            return [];
        }

        List<List<Vector2>> files = [];
        List<Vector2> fileCoordinates = [];

        Console.WriteLine();
        Console.WriteLine($"Attempting to read files from {graphDirectory}");
        Console.WriteLine();

        foreach (string filepath in filepaths)
        {
            stopwatch.Restart();

            string[] lines = File.ReadAllLines(filepath);
            int failed = 0;

            foreach (string line in lines)
            {
                string[] coordinates = line.Split(',');

                if (coordinates.Length != 2
                    || float.TryParse(coordinates[0], out float x) is false
                    || float.TryParse(coordinates[1], out float y) is false)
                {
                    failed++;
                    continue;
                }

                fileCoordinates.Add(new(x, y));
            }

            if (fileCoordinates.Count == 0)
            {
                Console.WriteLine($"No coordinates were able to be read from {Path.GetFileName(filepath)}");
                files.Add([]);

                stopwatch.Stop();
                Runtimes.ReadFileTimes.Add(stopwatch.Elapsed.TotalNanoseconds);

                continue;
            }

            files.Add(fileCoordinates);
            fileCoordinates.Clear();

            stopwatch.Stop();
            Runtimes.ReadFileTimes.Add(stopwatch.Elapsed.TotalNanoseconds);

            Console.Write($"{Path.GetFileName(filepath),-30} - ");
            Console.Write($"{lines.Length - failed,6:N0} coordinates");
            Console.WriteLine($" {((failed > 0) ? $", {failed,3:N0} failed" : "")}");
        }

        Console.WriteLine();
        Console.WriteLine($"Read {filepaths.Length} file{((filepaths.Length == 1) ? "" : "s")}");

        return files;
    }
}