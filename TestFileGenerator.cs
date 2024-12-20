using System;
using System.IO;
using System.Linq;

class TestFileGenerator
{
    private static readonly Random Random = new Random();

    public static void GenerateFile(string filePath, long fileSizeInBytes)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            long currentSize = 0;

            while (currentSize < fileSizeInBytes)
            {
                string line = GenerateLine();
                currentSize += line.Length + Environment.NewLine.Length;
                writer.WriteLine(line);
            }
        }
    }

    private static string GenerateLine()
    {
        int number = Random.Next(1, 100000); 
        string[] sampleStrings = { "Apple", "Banana is yellow", "Cherry is the best", "Something something something" };
        string str = sampleStrings[Random.Next(sampleStrings.Length)];
        return $"{number}. {str}";
    }

    static void Main(string[] args)
    {
        string outputPath = "testfile.txt";
        long fileSizeInBytes = 10L * 1024 * 1024;
        GenerateFile(outputPath, fileSizeInBytes);
        Console.WriteLine($"Test file generated at {outputPath}");
    }
}
