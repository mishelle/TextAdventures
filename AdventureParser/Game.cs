using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventureParser
{
    public class Game
    {
        //private fields for gamedef and gamestate
        private GameDefinition _gameDef;

        public Game(Stream gameDef) : this(gameDef, null)
        {
        }
        public Game(Stream gameDef, Stream gameState)
        {
            //load internal GameDefinition from stream
            //if gameState is not null, load that; else default based on gameDef
        }
        internal Game(GameDefinition gameDef)
        {
            _gameDef = gameDef;
        }

        public void LoadGameState(Stream gameState)
        {
            //pass stream to gamestate, or read as json and create object
        }
        public void SaveGameState(Stream gameState)
        {

        }
        public IEnumerable<string> ProcessAction(string input)
        {
            //get verb index, noun index from input
            //loop through all the action responses in the game.  this knows how to run its conditions and actions
            yield break; 
        }
    }
}
