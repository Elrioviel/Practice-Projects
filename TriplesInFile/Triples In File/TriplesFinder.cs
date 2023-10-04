using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Triples_In_File
{
    public class TriplesFinder
    {
        public static ConcurrentDictionary<string, int> FindTriples(string content)
        {
            var triplesFrequency = new ConcurrentDictionary<string, int>();
            Parallel.For(0, content.Length - 2, i =>
             {
                 string tripleLetters = new string(content.Skip(i).Take(3).ToArray());
                 triplesFrequency.AddOrUpdate(tripleLetters, key => 1, (key, value) => value + 1);
             });

            return triplesFrequency;
        }
        public static Dictionary<string, int> SortDictionary(ConcurrentDictionary<string, int> unsortedDictionary)
        {
            return unsortedDictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public static Dictionary<string, int> GetTenMostFrequentTriples(string content)
        {
            return SortDictionary(FindTriples(content)).Take(10).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
