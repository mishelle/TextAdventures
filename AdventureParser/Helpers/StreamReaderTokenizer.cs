using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureParser.Helpers
{
    public class StreamReaderTokenizer
    {
        private StreamReader _reader;
        private string[] _currentLine;
        private int _index;

        public StreamReaderTokenizer(StreamReader sr)
        {
            _reader = sr;
        }
        public int GetNextInt()
        {
            var token = getNextToken();
            return Int32.Parse(token);
        }
        public int[] GetNextInt(int count)
        {
            var result = new int[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = GetNextInt();
            }
            return result;
        }
        public string GetNextString()
        {
            string result = getNextToken();
            if (!result.StartsWith("\""))
                throw new FormatException();

            while(result.Length < 2 || !result.EndsWith("\""))
            {
                result += (_currentLine == null) ? "\n" : " ";
                result += getNextToken();
            }
            return result.Substring(1, result.Length - 2).Trim();
        }

        private string getNextToken()
        {
            string token = "";
            for ( ; token != null && token.Length == 0; token = getNextTokenOrEmpty()) ;

            if (token == null)
                throw new EndOfStreamException();
            return token;
        }
        private string getNextTokenOrEmpty()
        {
            while (_currentLine == null || _currentLine.Length == 0) {
                if (_reader.Peek() <= 0) return null;

                var currLine = _reader.ReadLine();
                currLine = currLine.Replace("\"\"", "\" \"");

                _currentLine = currLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                _index = 0;
            }
            string value = _currentLine[_index].Trim();

            if (++_index >= _currentLine.Length)
                _currentLine = null;
            return value;
        }

    }
}
