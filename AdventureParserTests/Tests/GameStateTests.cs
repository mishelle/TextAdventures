using AdventureParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TextAdventuresTests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void GameStateSerializes()
        {
            var gameState = new GameState(5);

            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(GameState));

            ser.WriteObject(stream1, gameState);
            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            var result = sr.ReadToEnd();
            var expected = "{\"BitFlags\":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false],\"Counters\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"CurrentCounter\":0,\"CurrentRoom\":0,\"IsDark\":false,\"ItemLocations\":[0,0,0,0,0],\"LightTimeRemaining\":0,\"RoomSaved\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"SavedRoom\":0}";
            Debug.WriteLine(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GameStateDeserializes()
        {
            var data = @"{""BitFlags"":[false,false,false,true,false,false,false,false,false,false,false, 
                    false,false,false,false,false,false,false,false,false,false,false,false,false,false,
                    false,false,false,false,false,false,true],
                    ""Counters"":[0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0],""CurrentCounter"":6,
                    ""CurrentRoom"":7,""IsDark"":true,""ItemLocations"":[8,9,0,10,110,0],
                    ""LightTimeRemaining"":25,""RoomSaved"":[0,111,0,0,0,0,0,0,0,0,0,0,0,0,0,0],""SavedRoom"":17}";
            var ser = new DataContractJsonSerializer(typeof(GameState));
            GameState gameState;
            using (var stream = GenerateStreamFromString(data))
            {
                gameState = (GameState)ser.ReadObject(stream);
            }


            Assert.AreEqual(7, gameState.CurrentRoom);
            Assert.AreEqual(true, gameState.IsDark);
            Assert.AreEqual(8, gameState.ItemLocations[0]);
            Assert.AreEqual(0, gameState.ItemLocations[5]);
            Assert.AreEqual(25, gameState.LightTimeRemaining);
            Assert.AreEqual(111, gameState.RoomSaved[1]);
            Assert.AreEqual(17, gameState.SavedRoom);
        }

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
