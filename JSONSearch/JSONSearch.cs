using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    [CSBootcamp("JSONSearchAttribute")]
    public class JSONSearch : ISearch
    {
        public string Type => "JSON";

        public List<SearchResult> Search(string query, string root, string type, bool subDir)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.json", subDir == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
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

        // returns any valid json file that has the key-value in query 
        public List<SearchResult> SearchByContent(string query, string root, string type, bool subDir)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.json", subDir == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            string[] splited = query.Split(':');
            var key = splited[0];
            var value = splited[1].Trim();

            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                var filePath = file;
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    var jsonObj = JObject.Parse(jsonContent);
                    
                    if (jsonObj != null && jsonObj.ContainsKey(key) && jsonObj[key].ToString() == value) {
                        results.Add(new SearchResult(fileName, filePath, jsonContent));
                    }
                    
                }catch(Exception ex) {
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
