using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class OperationParser
    {
        public OperationParser()
        {

        }

        public override Expression ParseExpression(TextReader textReader)
        {
            string operation = base._expressionReader.ReadExpression(textReader);
        }
    }
}
