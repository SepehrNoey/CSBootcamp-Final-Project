using SearchInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSearch
{
    public class TextSearch : ISearch
    {
        private List<SearchResult> results = new List<SearchResult>();

        public string Type => "TXT";

        public List<SearchResult> Search(string query, string root, string type)
        {
            find_all(query, root, type);
            var toBeReturned = new List<SearchResult>(results);
            results.Clear();
            return toBeReturned;
        }

        private void find_all(string query, string root, string type) {
            string[] files = Directory.GetFiles(root);
            foreach (string file in files)
            {
                var file_name = Path.GetFileName(file);
                var file_path = file;
                if (file_name.Contains(query))
                {
                    results.Add(new SearchResult(file_name, file_path));
                }
            }

            string[] subDirs = Directory.GetDirectories(root);
            foreach (string subdir in subDirs)
            {
                find_all(query, subdir, type);
            }

        }
    }
}
