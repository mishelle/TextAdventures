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
            //this should fail if the next token is not in quotes
            string result = getNextToken();
            if (!result.StartsWith("\""))
                throw new FormatException();

            return result.Substring(1, result.Length - 2).Trim();
        }

        private string getNextToken()
        {
            string token = "";
            bool inString = false;
            bool done = false;

            while (!done)
            {
                char nextChar = (char)_reader.Peek();
                bool skip = false;
                bool isWS = isWhitespace(nextChar);

                if (nextChar == 65535)
                {
                    if (token.Length == 0 || inString)
                        throw new EndOfStreamException();

                    break;
                }

                if (inString && nextChar == '"') 
                    done = true;//still consume and add

                if (token.Length == 0) {
                    if (isWS)
                        skip = true;//consume don't add
                    if (nextChar == '"')
                        inString = true;    //still consume and add
                }
                else if (!inString)
                {
                    if (isWS)
                    {
                        done = true;
                        skip = true;//consume don't add
                    }
                    else if (nextChar == '"')
                        break;  //don't consume
                }

                char thisChar = (char)_reader.Read();
                if (!skip)
                {
                    token += thisChar;
                }
            }

            return token;
        }

        private bool isWhitespace(char c)
        {
            return c == ' ' || c == '\t' || c == '\n';
        }

    }
}
