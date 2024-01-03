using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Bootcamp_Fianl_Project
{
    internal class SearchDistributer
    {
        public List<Task<List<SearchResult>>> Distribute(Dictionary<string, ISearch> engines, string query, string path, string[] wantedTypes)
        {
            var searchers = new List<Task<List<SearchResult>>>();

            // adding searchers for each type in current directory (not subdirectories)
            foreach (var type in wantedTypes)
                searchers.Add(new Task<List<SearchResult>>(() => engines[type].Search(query, path, type, false)));

            // adding task for each each 3 subdirectories and type
            var subDirs = Directory.GetDirectories(path);
            for (int i = 0; i < subDirs.Length; i += 3)
            {
                foreach (var type in wantedTypes)
                {
                    var t = new Task<List<SearchResult>>(() =>
                    {
                        var currResults = new List<SearchResult>();
                        if (i < subDirs.Length)
                            currResults.AddRange(engines[type].Search(query, subDirs[i], type, true));
                        if (i + 1 < subDirs.Length)
                            currResults.AddRange(engines[type].Search(query, subDirs[i + 1], type, true));
                        if (i + 2 < subDirs.Length)
                            currResults.AddRange(engines[type].Search(query, subDirs[i + 2], type, true));

                        return currResults;
                    });

                    searchers.Add(t);
                }
            }

            return searchers;
        }
    }
}
