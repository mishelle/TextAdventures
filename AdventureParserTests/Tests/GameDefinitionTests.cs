using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AdventureParser;
using System.Diagnostics;

namespace AdventureParserTests
{
    [TestClass]
    public class GameDefinitionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var path = @"C:\Users\mgran_000\Documents\Adventure Games\";
            var file = "sampler1.dat";

            using (StreamReader sr = new StreamReader(Path.Combine(path, file)))
            {
                var db = new GameDefinition(sr);
                Debug.WriteLine(db.NumItems);
                Debug.WriteLine(db.StartingRoom);
                Assert.Equals(66, db.NumItems);
            }
        }

    }
}
