using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParserTests.Tests
{
    public static class Helper
    {
        public static void assertThrowsException<T>(Action run) where T : Exception
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
