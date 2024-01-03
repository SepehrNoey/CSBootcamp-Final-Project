using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInterface
{
    public class CSBootcampAttribute : Attribute
    {
        public string Name { get; }
        public CSBootcampAttribute(string name)
        {
            Name = name;
        }
    }
}
