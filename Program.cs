using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Generating test file...");
        //TestFileGenerator.GenerateFile("testfile.txt", 10L * 1024 * 1024); // 10 MB
        //Console.WriteLine("Test file generated successfully!");

        Console.WriteLine("Sorting test file...");
        LargeFileSorter.SortLargeFile("testfile.txt", "sortedfile.txt", @"C:\\Users\\artur.ghidora\\source\\repos\\sorting task\\bin\\Debug\\net8.0");
        Console.WriteLine("File sorted successfully!");

    }
}
