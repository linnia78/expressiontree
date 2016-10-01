using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class NumericReader : ExpressionReader, IExpressionReader
    {
        public NumericReader() : base((@char) => char.IsDigit(@char) || @char == '.') { }

        public override string ReadExpression(TextReader reader)
        {
            return base.ReadWhile(reader, base._readCondition);
        }
    }
}
