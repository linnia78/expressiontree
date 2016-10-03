using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class NumericParser : ExpressionParser
    {
        public NumericParser(IExpressionReader expressionReader) : base(expressionReader, char.IsDigit) { }

        public override Expression ParseExpression(TextReader textReader)
        {
            decimal result;
            string expression = base._expressionReader.ReadExpression(textReader);
            if (decimal.TryParse(expression, out result))
            {
                return Expression.Constant(result);
            }
            else
            {
                throw new InvalidCastException($"Unable to convert {expression} to numeric.");
            }
        }
    }
}
