using AdventureParser.Helpers;
using AdventureParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser
{
    internal class GameDefinition
    {
        internal readonly int LightItemIndex = 9;

        private GameHeader _header;
        public int NumItems { get { return Items.Length; } }
        public int StartingRoom { get { return _header.StartingRoom; } }
        public int LightRefill { get { return _header.LightRefill; } }
        private Item[] Items;
        private string[] Messages;
        private Location[] Locations;
        private ActionResponse[] ActionResponses;
        private string[] Verbs;
        private string[] Nouns;

        public GameDefinition(StreamReader sr)
        {
            var tokenizer = new StreamReaderTokenizer(sr);
            var dummy = tokenizer.GetNextInt();
            var header = new GameHeader(tokenizer.GetNextInt(12));
            _header = header;

            ActionResponses = loadActions(header.NumActions, tokenizer);

            var words = loadWords(header.NumWords, tokenizer);
            Verbs = words[0];
            Nouns = words[1];

            Locations = loadLocations(header.NumLocations, tokenizer);

            Messages = loadMessages(header.NumMessages, tokenizer);

            Items = loadItems(header.NumItems, header.NumLocations, tokenizer);

            loadActionComments(ActionResponses, tokenizer);

            var version = tokenizer.GetNextInt();
            var extra = tokenizer.GetNextInt();
        }

        private void loadActionComments(ActionResponse[] actionResponses, StreamReaderTokenizer tokenizer)
        {
            for (var i = 0; i < actionResponses.Length; i++)
            {
                var comment = tokenizer.GetNextString();
            }
        }

        private Item[] loadItems(int count, int numLocations, StreamReaderTokenizer tokenizer)
        {
            var result = new Item[count];
            for (var i = 0; i < count; i++)
            {
                //todo: make sure the location does not exceed the number of locations
                result[i] = new Item(tokenizer.GetNextString(), tokenizer.GetNextInt());
            }
            return result;
        }

        private string[] loadMessages(int count, StreamReaderTokenizer tokenizer)
        {
            var result = new string[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = tokenizer.GetNextString();
            }
            return result;
        }

        private Location[] loadLocations(int count, StreamReaderTokenizer tokenizer)
        {
            var result = new Location[count];
            for (var i = 0; i < count; i++)
            {
                //todo: ensure each exit points to a valid room
                result[i] = new Location(tokenizer.GetNextInt(6), tokenizer.GetNextString());
            }
            return result;
        }

        private string[][] loadWords(int count, StreamReaderTokenizer tokenizer)
        {
            var verbs = new string[count];
            var nouns = new string[count];
            for (var i = 0; i < count; i++)
            {
                verbs[i] = tokenizer.GetNextString();
                nouns[i] = tokenizer.GetNextString();
            }
            return new string[2][] {verbs, nouns};
        }

        private ActionResponse[] loadActions(int count, StreamReaderTokenizer tokenizer)
        {
            var result = new ActionResponse[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = new ActionResponse(tokenizer.GetNextInt(), tokenizer.GetNextInt(5), tokenizer.GetNextInt(2));
            }
            return result;
        }
        
        //this will have the knowledge of the word lists, autoget, action fallthrough?
        //expose an interface that is independent of the storage structure

        internal int GetLocationOfItem(int i)
        {
            //check 0 <= i < Items.Length
            return Items[i].LocationIndex;
        }
    }
}
