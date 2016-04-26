using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Model
{
    internal class Location
    {
        internal string Description { get; private set; }
        internal int[] Exits { get; private set; }

        internal Location(int[] exits, string desc)
        {
            Description = desc;
            Exits = exits;
            //Exits = new int[6];
            //for (var i = 0; i < 6; i++)
            //{
            //    if (exits.Length >= i)
            //        Exits[i] = exits[i];
            //}
        }
    }
}
