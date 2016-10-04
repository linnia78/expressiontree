using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ParamParser : StatementParser
    {
        public ParamParser(IStatementReader expressionReader) : base(expressionReader, (@char) => @char == '[') { }
        public override Statement ParseStatement(TextReader textReader, Expression param)
        {
            string statement = base._expressionReader.ReadStatement(textReader);
            var keyParam = Expression.Constant(statement);
            var result = Expression.Parameter(typeof(string), "result");
            var block = Expression.Block(new[] { result }, Expression.Assign(result, Expression.Property(param, "Item", keyParam)), result);
            return Statement.CreateStatement(typeof(string), block);
        }
    }
}
