using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Model
{
    internal class GameHeader
    {
        internal int WordLength { get; private set; }
        internal int MaxCarry { get; private set; }
        internal int LightRefill { get; private set; }
        internal int NumTreasures { get; private set; }
        internal int TreasureRoom { get; private set; }
        internal int StartingRoom { get; private set; }
        internal int NumLocations { get; private set; }
        internal int NumItems { get; private set; }
        internal int NumMessages { get; private set; }
        internal int NumActions { get; private set; }
        internal int NumWords { get; private set; }


        internal GameHeader(int[] args)
        {
            //count must be >10
            //all must be > 0
            NumItems = args[0] + 1;
            NumActions = args[1] + 1;
            NumWords = args[2] + 1;
            NumLocations = args[3] + 1;
            MaxCarry = args[4];
            StartingRoom = args[5];//must be less than numRooms
            NumTreasures = args[6];
            WordLength = args[7];
            LightRefill = args[8];
            NumMessages = args[9] + 1;
            TreasureRoom = args[10];//must be less than numRooms
        }
    }
}
