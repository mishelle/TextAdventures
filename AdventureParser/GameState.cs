using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser
{
    [DataContract]
    internal class GameState
    {
        [DataMember] 
        public int[] ItemLocations;
        [DataMember] 
        public int[] Counters = new int[16];
        [DataMember] 
        public int[] RoomSaved = new int[16];
        [DataMember]
        public bool[] BitFlags = new bool[32];
        [DataMember]
        public bool IsDark { get; set; }
        [DataMember]
        public int SavedRoom { get; set; }
        [DataMember]
        public int CurrentRoom { get; set; }
        [DataMember]
        public int CurrentCounter { get; set; }
        [DataMember]
        public int LightTimeRemaining { get; set; }

        public GameState(GameDefinition gameDef) : this(gameDef.NumItems)
        {
            for (var i = 0; i < gameDef.NumItems; i++)
                ItemLocations[i] = gameDef.GetLocationOfItem(i);
            CurrentRoom = gameDef.StartingRoom;
            LightTimeRemaining = gameDef.LightRefill;
        }

        internal GameState(int numItems)
        {
            ItemLocations = new int[numItems];
        }

        //constructor from a Stream?  or json annotation?
        //tests either way
    }
}
