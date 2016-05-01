using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;
using AdventureParser.Model;

namespace AdventureParserTests.Tests
{
    [TestClass]
    public class FindWordTests
    {
        //
        // TODO: figure out where findWord function should go
        //maybe an object with verbs and nouns? 
        //
             
        [TestMethod]
        public void FindNullFails()
        {
            //given a list of words (word length)?
            var data = new string[] { "AAA", "*BBB", "CCC" };
            var wordList = new WordList(data);
            Helper.assertThrowsException<ArgumentException>(() =>
            {
                var result = wordList.findWordIndex(null);
            });
        }

        [TestMethod]
        public void FindExactValueWorks()
        {
            var data = new string[] { "AAA", "*BBB", "CCC" };
            var wordList = new WordList(data);
            var result = wordList.findWordIndex("CCC");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FindInexactValueWorks()
        {
            var data = new string[] { "AAA", "*BBB", "CCC", "DDD" };
            var wordList = new WordList(data);
            var result = wordList.findWordIndex("ccc123");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void FindMissingValueReturnsMinus1()
        {
            var data = new string[] { "AAA", "*BBB", "CCC", "DDD" };
            var wordList = new WordList(data);
            var result = wordList.findWordIndex("cabbage");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void FindWordFindsSynonym()
        {
            var data = new string[] { "AAA", "BBB", "*CCC", "DDD" };
            var wordList = new WordList(data);
            var result = wordList.findWordIndex("ccccc");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void FindWordFindsFirstSynonym()
        {
            var data = new string[] { "AAA", "BBB", "*CCC", "*DDD", "EEE" };
            var wordList = new WordList(data);
            var result = wordList.findWordIndex("dddd");
            Assert.AreEqual(1, result);
        }
    }
}
