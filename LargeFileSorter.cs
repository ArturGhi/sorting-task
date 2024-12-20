using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LargeFileSorter
{
    public static void SortLargeFile(string inputFilePath, string outputFilePath, string tempDirectory)
    {
        const int chunkSize = 100000;
        List<string> tempFiles = new List<string>();

        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            while (!reader.EndOfStream)
            {
                List<string> lines = new List<string>();

                for (int i = 0; i < chunkSize && !reader.EndOfStream; i++)
                {
                    lines.Add(reader.ReadLine());
                }

                lines.Sort(CompareLines);
                string tempFile = Path.Combine(tempDirectory, Guid.NewGuid().ToString() + ".txt");

                File.WriteAllLines(tempFile, lines);
                tempFiles.Add(tempFile);
            }
        }

        MergeSortedFiles(tempFiles, outputFilePath);

        foreach (string tempFile in tempFiles)
        {
            File.Delete(tempFile);
        }
    }

    private static int CompareLines(string line1, string line2)
    {
        string[] parts1 = line1.Split(new[] { ". " }, 2, StringSplitOptions.None);
        string[] parts2 = line2.Split(new[] { ". " }, 2, StringSplitOptions.None);

        int stringCompare = string.Compare(parts1[1], parts2[1], StringComparison.Ordinal);
        if (stringCompare != 0)
            return stringCompare;

        int number1 = int.Parse(parts1[0]);
        int number2 = int.Parse(parts2[0]);

        return number1.CompareTo(number2);
    }

    private static void MergeSortedFiles(List<string> sortedFiles, string outputFilePath)
    {
        PriorityQueue<(string line, StreamReader reader), string> priorityQueue = new PriorityQueue<(string, StreamReader), string>();

        foreach (string file in sortedFiles)
        {
            StreamReader reader = new StreamReader(file);
            if (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                priorityQueue.Enqueue((line, reader), line);
            }
        }

        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            while (priorityQueue.Count > 0)
            {
                var (line, reader) = priorityQueue.Dequeue();
                writer.WriteLine(line);

                if (!reader.EndOfStream)
                {
                    string nextLine = reader.ReadLine();
                    priorityQueue.Enqueue((nextLine, reader), nextLine);
                }
                else
                {
                    reader.Dispose();
                }
            }
        }
    }

    public static void Main1()
    {
        string inputFilePath = "testfile.txt";
        string outputFilePath = "sortedfile.txt";
        string tempDirectory = "temp";

        if (!Directory.Exists(tempDirectory))
            Directory.CreateDirectory(tempDirectory);

        SortLargeFile(inputFilePath, outputFilePath, tempDirectory);
        Console.WriteLine($"Sorted file {outputFilePath}");
    }
}
