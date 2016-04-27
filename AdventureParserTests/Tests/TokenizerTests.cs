
using AdventureParser.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventuresTests.Tests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void GetIntFailsWhenNoContents()
        {
            var contents = "";
            runTest(contents, (tester) =>
            {
                assertThrowsException<EndOfStreamException>(() =>
                {
                    var value = tester.GetNextInt();
                });

            });
        }

        [TestMethod]
        public void GetIntFailsWhenNoInt()
        {
            var contents = "blah 123";
            runTest(contents, (tester) =>
            {
                assertThrowsException<FormatException>(() => {
                    var value = tester.GetNextInt();
                });
                
            });

        }
        [TestMethod]
        public void GetIntGetsSingleInts()
        {
            var contents = "123 1 45";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextInt();
                Assert.AreEqual(123, value);
                value = tester.GetNextInt();
                Assert.AreEqual(1, value);
                value = tester.GetNextInt();
                Assert.AreEqual(45, value);
            });
            
        }

        [TestMethod]
        public void GetIntArrayFailsWhenNoContents()
        {
            var contents = "";
            runTest(contents, (tester) =>
            {
                assertThrowsException<EndOfStreamException>(() =>
                {
                    var array = tester.GetNextInt(2);
                });

            });
        }

        [TestMethod]
        public void GetIntArrayFailsWhenNoInt()
        {
            var contents = "12 blah 123";
            runTest(contents, (tester) =>
            {
                assertThrowsException<FormatException>(() =>
                {
                    var array = tester.GetNextInt(3);
                });

            });

        }
        [TestMethod]
        public void GetIntArrayGetsInts()
        {
            var contents = "123 1 45";
            runTest(contents, (tester) =>
            {
                var array = tester.GetNextInt(3);
                Assert.AreEqual(123, array[0]);
                Assert.AreEqual(1, array[1]);
                Assert.AreEqual(45, array[2]);
            });

        }
        [TestMethod]
        public void GetIntArrayGetsIntsFromMultipleLines()
        {
            var contents = "123 \n\n1 \n  45";
            runTest(contents, (tester) =>
            {
                var array = tester.GetNextInt(3);
                Assert.AreEqual(123, array[0]);
                Assert.AreEqual(1, array[1]);
                Assert.AreEqual(45, array[2]);
            });

        }

        [TestMethod]
        public void GetIntArrayFailsWhenNotEnoughValues()
        {
            var contents = "123 1 45";
            runTest(contents, (tester) =>
            {
                assertThrowsException<EndOfStreamException>(() =>
                {
                    var array = tester.GetNextInt(4);
                });
            });

        }
        [TestMethod]
        public void GetStringFailsWhenNoContents()
        {
            var contents = "";
            runTest(contents, (tester) =>
            {
                assertThrowsException<EndOfStreamException>(() =>
                {
                    var value = tester.GetNextString();
                });

            });
        }
        [TestMethod]
        public void GetStringFailsWhenNoStartingQuote()
        {
            var contents = "asdf sdf";
            runTest(contents, (tester) =>
            {
                assertThrowsException<FormatException>(() =>
                {
                    var value = tester.GetNextString();
                });

            });
        }
        [TestMethod]
        public void GetStringFailsWhenNoEndingingQuote()
        {
            var contents = "\"asdf sdf";
            runTest(contents, (tester) =>
            {
                assertThrowsException<EndOfStreamException>(() =>
                {
                    var value = tester.GetNextString();
                });

            });
        }
        [TestMethod]
        public void GetStringGetsSingleWordNoQuotes()
        {
            var contents = "\"asdf\" fdsa";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("asdf", value);

            });
        }
        [TestMethod]
        public void GetStringNoSpacesGetsEmptyWord()
        {
            var contents = "\"\"\"fdsa\"";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("", value);

            });
        }
        [TestMethod]
        public void GetStringNoSpacesGetsSingleWord()
        {
            var contents = "\"asdf\"\"fdsa\"";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("asdf", value);

            });
        }

        [TestMethod]
        public void GetStringGetsSingleTrimmedWordNoQuotes()
        {
            var contents = "  \" asdf  \"  fdsa";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("asdf", value);

            });
        }
        [TestMethod]
        public void GetStringGetsMultipleWordsNoQuotes()
        {
            var contents = "\"This is a string.\" \"another string\"";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("This is a string.", value);

            });
        }
        [TestMethod]
        public void GetStringGetsMultipleLinesNoQuotes()
        {
            //do I want to keep cr or change it to a space?
            var contents = "\"This is \n a string.\"";
            runTest(contents, (tester) =>
            {
                var value = tester.GetNextString();
                Assert.AreEqual("This is \n a string.", value);

            });
        }

        //get string - single token with quotes
        //get string - multiple tokens with quotes
        //get string - multiple tokens with quotes and cr
        //get string - eof
        private void runTest(string contents, Action<StreamReaderTokenizer> run)
        {
            using (var stream = getStream(contents))
            {
                using (var sr = new StreamReader(stream))
                {
                    var tester = new StreamReaderTokenizer(sr);
                    run(tester);
                }
            }

        }
        private Stream getStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        private void assertThrowsException<T>(Action run) where T : Exception
        {
            try
            {
                run();
                Assert.Fail("expected error of type " + typeof(T).Name);
            }
            catch (T)
            {
                //good
            }
        }
    }
}
