using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Triples_In_File;

namespace TriplesInFileUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TriplesInFileFinder triplesInMyFile = new TriplesInFileFinder(@"E:\Practice for interview\Projects\TriplesInFile\TriplesInFile\123.txt");
            Dictionary<string, int> result = await triplesInMyFile.FindTriplesInFile();
            DisplayResult(result);
            stopwatch.Stop();
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalMilliseconds}");
            Console.ReadLine();
        }
        public static void DisplayResult(Dictionary<string, int> triplets)
        {
            foreach (var triplet in triplets)
            {
                Console.WriteLine($"{triplet.Key} occurence:{triplet.Value};");
            }
        }
    }
}
