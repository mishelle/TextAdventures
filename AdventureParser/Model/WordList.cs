using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Model
{
    internal class WordList
    {
        private readonly string[] list;

        internal WordList(string[] words)
        {
            list = words;
        }
        internal string getWord(int index)
        {
            if (index < 0 || index >= list.Length)
                throw new ArgumentException();
            return list[index];
        }

        internal int findWordIndex(string input)
        {
            //Contract.Requires<ArgumentException>(input != null);
            if (input == null) throw new ArgumentException();
            int foundIndex = 0;
            for (var i = 0; i < list.Length; i++)
            {
                string testWord = list[i];
                if (testWord.StartsWith("*"))
                    testWord = testWord.Substring(1);
                else
                    foundIndex = i;     //list[i] is a non-synonym

                if (input.ToUpper().StartsWith(testWord))
                    return foundIndex;
            }
            return -1;
        }
    }
}
