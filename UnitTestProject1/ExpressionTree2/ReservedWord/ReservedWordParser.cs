using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ReservedWordParser : ExpressionParser
    {

        public ReservedWordParser(IExpressionReader expressionReader) : base(expressionReader, char.IsLetter) { }

        public override Expression ParseExpression(TextReader reader)
        {
            string expression = this._expressionReader.ReadExpression(reader);
            return ((ReservedWord)expression).Expression;
        }
    }
}
