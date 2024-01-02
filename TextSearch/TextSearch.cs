﻿using SearchInterface;
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
        public string Type => "TXT";

        public List<SearchResult> Search(string query, string root, string type)
        {
            var results = new List<SearchResult>();
            string[] files = Directory.GetFiles(root, "*.txt", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                var file_name = Path.GetFileName(file);
                var file_path = file;
                if (file_name.Contains(query))
                {
                    results.Add(new SearchResult(file_name, file_path));
                }
            }

            return results;
        }
    }
}
