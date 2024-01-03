using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextSearch
{
    [CSBootcamp("TextSearchAttribute")]
    public class TextSearch : ISearch
    {
        public string Type => "TXT";

        public List<SearchResult> Search(string query, string root, string type, bool subDir)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.txt", subDir == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            
            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                var filePath = file;

                if (matches(query, fileName))
                {
                    var content = File.ReadAllText(filePath);
                    results.Add(new SearchResult(fileName, filePath, content));
                }
            }

            return results;
        }

        public List<SearchResult> SearchByContent(string query, string root, string type, bool subDir)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.txt", subDir == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                var filePath = file;
                try
                {
                    string content = File.ReadAllText(filePath);
                    if (content.Contains(query))
                        results.Add(new SearchResult(fileName, filePath, content));

                }
                catch (Exception ex)
                {
                    continue;
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
