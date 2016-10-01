using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class NumericParser : IExpressionParser
    {
        private readonly ExpressionReader _reader;
        private readonly Func<char, bool> _parseCondition;

        public NumericParser(ExpressionReader reader)
        {
            this._reader = reader;
            this._parseCondition = char.IsDigit;
        }

        public bool CanParse(char @char)
        {
            return this._parseCondition(@char);
        }

        public Expression ReadExpression(TextReader reader)
        {
            decimal result;
            string expression = this._reader.ReadExpression(reader);
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
