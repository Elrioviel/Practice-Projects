using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Triples_In_File
{
    public class TriplesInFileFinder : TriplesFinder
    {
        private readonly string _filePath;
        public TriplesInFileFinder(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<Dictionary<string, int>> FindTriplesInFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var fileContent = await ReadFileAsync(_filePath);
                    return GetTenMostFrequentTriples(fileContent);
                }
                else
                {
                    Console.WriteLine($"File doesn't exist in path: {_filePath}");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Dictionary<string, int>();
        }
        private async Task<string> ReadFileAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fileStream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
