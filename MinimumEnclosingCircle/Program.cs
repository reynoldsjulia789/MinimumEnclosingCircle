using System.Numerics;
using System.Reflection.Metadata;

namespace MinimumEnclosingCircle;

public class Program
{
    private const string graphDirectory = "../Graphs";

    public static void Main()
    {
        PrintRunInstructions();

        // read points
        List<ISet<Vector2>> filecontents = GetFileContents();

        // find min enclosing circle

        // print results
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
    private static List<ISet<Vector2>> GetFileContents()
    {
        string[] filepaths = Directory.GetFiles(graphDirectory);

        if (filepaths.Length == 0)
        {
            Console.WriteLine($"No files found in directory ({graphDirectory})");
            return [];
        }

        List<ISet<Vector2>> files = [];
        HashSet<Vector2> fileCoordinates = [];

        Console.WriteLine();
        Console.WriteLine($"Attempting to read files from {graphDirectory}");
        Console.WriteLine();

        foreach (string filepath in filepaths)
        {
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
                continue;
            }

            files.Add(fileCoordinates);
            fileCoordinates.Clear();

            Console.Write($"{Path.GetFileName(filepath),-30} - ");
            Console.Write($"{lines.Length - failed,6:N0} coordinates");
            Console.WriteLine($" {((failed > 0) ? $", {failed,3:N0} failed" : "")}");
        }

        Console.WriteLine();
        Console.WriteLine($"Read {filepaths.Length} file{((filepaths.Length == 1) ? "" : "s")}");

        return files;
    }
}