using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Model
{
    internal class Item
    {
        internal string Description { get; private set; }
        internal int LocationIndex { get; private set; }
        internal string AutoGet { get; private set; }
        
        internal Item(string desc, int locationIndex)
        {
            LocationIndex = locationIndex;
            var values = desc.Split(new []{"/"}, StringSplitOptions.RemoveEmptyEntries);
            Description = values.Length > 0 ? values[0] : "";
            AutoGet = values.Length > 1 ? values[1] : "";
        }
    }
}
