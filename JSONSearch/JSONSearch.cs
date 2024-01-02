using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSONSearch
{
    public class JSONSearch : ISearch
    {
        public string Type => "JSON";

        public List<SearchResult> Search(string query, string root, string type)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.json", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                var filePath = file;
                if (matches(query, fileName))
                {
                    results.Add(new SearchResult(fileName, filePath));
                }
            }

            return results;
        }

        private bool matches(string query, string reference)
        {
            string escapedInput = Regex.Escape(query);
            string pattern = escapedInput.Replace("\\*", ".*");
            return Regex.IsMatch(reference, pattern);

        }

    }
}
