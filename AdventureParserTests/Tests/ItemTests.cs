using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventureParser.Model;

namespace AdventureParserTests.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void ItemWithNoAutogetCreated()
        {
            var item = new Item("bblah", 2);
            Assert.AreEqual("bblah", item.Description);
            Assert.AreEqual("", item.AutoGet);
        }
        [TestMethod]
        public void ItemWithAutogetCreated()
        {
            var item = new Item("Chiggers/CHI/", 2);
            Assert.AreEqual("Chiggers", item.Description);
            Assert.AreEqual("CHI", item.AutoGet);
        }
        [TestMethod]
        public void ItemWithNoDescriptionCreated()
        {
            var item = new Item("", 2);
            Assert.AreEqual("", item.Description);
            Assert.AreEqual("", item.AutoGet);
        }
    }
}
