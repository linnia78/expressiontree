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
        private static readonly List<char> FirstLevelSymbol = Operation.Operations.Select(x => x.Key[0]).ToList();
        private static readonly List<char> SecondLevelSymbol = Operation.Operations.Where(x => x.Key.Length > 1).Select(x => x.Key[1]).ToList();
        private static readonly List<char> ThirdLevelSymbol = Operation.Operations.Where(x => x.Key.Length > 2).Select(x => x.Key[2]).ToList();
        
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
