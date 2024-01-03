using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInterface
{
    public class SearchResult
    {
        public SearchResult(string name, string path, string content = "Default-Content") {
            this.Name = name;
            this.Path = path;
            this.Content = content;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Content {  get; set; }

        public override string ToString()
        {
            return $"name: {Name}\npath: {Path}\ncontent: {Content}";
        }
    }
}
