using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInterface
{
    public class SearchResult
    {
        public SearchResult(string name, string path) {
            this.Name = name;
            this.Path = path;
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"name: {Name}\npath: {Path}";
        }
    }
}
