using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public interface IExpressionParser
    {
        bool CanParse(char @char);
        Expression ParseExpression(TextReader reader);
    }
    public abstract class ExpressionParser : IExpressionParser
    {
        protected readonly IExpressionReader _expressionReader;
        protected readonly Func<char, bool> _parseCondition;
        public ExpressionParser(IExpressionReader expressionReader, Func<char, bool> parseCondition)
        {
            this._expressionReader = expressionReader;
            this._parseCondition = parseCondition;
        }

        public virtual bool CanParse(char @char) { return _parseCondition(@char); }
        public abstract Expression ParseExpression(TextReader textReader);

    }
}
