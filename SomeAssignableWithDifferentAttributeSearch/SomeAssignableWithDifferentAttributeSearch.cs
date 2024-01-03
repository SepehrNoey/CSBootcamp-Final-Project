using SearchInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeAssignableWithDifferentAttributeSearch
{
    public class SomeAssignableWithDifferentAttributeSearch : ISearch
    {
        public string Type => "SomeAssignableWithDifferentAttributeSearch";

        public List<SearchResult> Search(string query, string root, string type, bool subDir)
        {
            return new List<SearchResult>(); // just for simplicity
        }

        public List<SearchResult> SearchByContent(string query, string root, string type, bool subDir)
        {
            throw new NotImplementedException();
        }
    }
}
