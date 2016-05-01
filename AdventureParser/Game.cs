using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AdventureParser
{
    public class Game
    {
        //private fields for gamedef and gamestate
        private GameDefinition _gameDef;
        private GameState _gameState;

        public Game(Stream gameDef) : this(gameDef, null)
        {
        }
        public Game(Stream gameDef, Stream gameState)
        {
            using (var sr = new StreamReader(gameDef))
            {
                _gameDef = new GameDefinition(sr);
                if (gameState != null)
                    LoadGameState(gameState);
                else
                    _gameState = new GameState(_gameDef);
            }
        }
        internal Game(GameDefinition gameDef)
        {
            _gameDef = gameDef;
        }

        public void LoadGameState(Stream gameState)
        {
            var ser = new DataContractJsonSerializer(typeof(GameState));
            _gameState = (GameState)ser.ReadObject(gameState);
            //check whether gs matches gd
        }
        public void SaveGameState(Stream gameState)
        {
            var ser = new DataContractJsonSerializer(typeof(GameState));
            ser.WriteObject(gameState, gameState);
        }
        public IEnumerable<string> ProcessAction(string input)
        {
            //get verb index, noun index from input and game def

            //loop through all the action responses in the game.  this knows how to run its conditions and actions
            yield break; 
        }
    }
}
