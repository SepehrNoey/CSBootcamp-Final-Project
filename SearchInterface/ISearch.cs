using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInterface
{
    public interface ISearch
    {
        string Type { get; }
        List<SearchResult> Search(string query, string root, string type, bool subDir);
        List<SearchResult> SearchByContent(string query, string root, string type, bool subDir);
    }
}
