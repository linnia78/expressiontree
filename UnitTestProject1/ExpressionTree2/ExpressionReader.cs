using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ExpressionReader
    {
        public static readonly ExpressionReader NumericReader = new NumericReader();

        protected readonly Func<char, bool> _readCondition;
        public ExpressionReader(Func<char, bool> readCondition)
        {
            this._readCondition = readCondition;
        }

        public virtual string ReadExpression(TextReader reader) { throw new NotImplementedException(); }
        
        protected string ReadWhile(TextReader reader, Func<char, bool> condition)
        {
            var expression = string.Empty;
            int peek;
            while ((peek = reader.Peek()) > -1)
            {
                var next = (char)peek;
                if (condition(next))
                {
                    expression += (char)reader.Read();
                }
                else
                {
                    break;
                }
            }
            return expression;
        }
    }
}
