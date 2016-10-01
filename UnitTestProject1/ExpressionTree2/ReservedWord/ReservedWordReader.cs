using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ReservedWordReader : ExpressionReader, IExpressionReader
    {
        public ReservedWordReader() : base(char.IsLetter) { }
        public override string ReadExpression(TextReader reader)
        {
            return base.ReadWhile(reader, (@char) => base._readCondition(@char));
        }
    }
}
