using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ReservedWord
    {
        public Expression Expression { get; private set; }

        public static readonly ReservedWord True = new ReservedWord(Expression.Constant(true));
        public static readonly ReservedWord False = new ReservedWord(Expression.Constant(false));

        public static readonly Dictionary<string, ReservedWord> Words = new Dictionary<string, ReservedWord>
        {
            { "true", True },
            { "false", False }
        };

        public ReservedWord(Expression expression)
        {
            this.Expression = expression;
        }

        public static explicit operator ReservedWord(string symbol)
        {
            ReservedWord result;

            if (Words.TryGetValue(symbol, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
