using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class OperationReader
    {
        public static bool IsOperation(TextReader reader)
        {
            return FirstLevelSymbol.Contains((char)reader.Peek());
        }

        public static Operation GetOperation(TextReader reader)
        {
            var operation = Parse(reader, FirstLevelSymbol.Contains, SecondLevelSymbol.Contains, ThirdLevelSymbol.Contains);
            return (Operation)operation;
        }

        private static string Parse(TextReader reader, params Func<char, bool>[] conditions)
        {
            string result = string.Empty;
            foreach(var condition in conditions)
            {
                if (condition((char)reader.Peek()))
                {
                    result += ((char)reader.Read());
                }
                else { break; }
            }
            return result;
        }
    }
}
