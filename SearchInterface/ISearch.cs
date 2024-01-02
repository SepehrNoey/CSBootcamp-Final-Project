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
        List<SearchResult> Search(string query, string root, string type);
        // more functions can be added later
    }
}
