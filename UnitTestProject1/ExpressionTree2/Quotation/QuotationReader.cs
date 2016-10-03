using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class QuotationReader : ExpressionReader
    {
        public QuotationReader() : base((@char) => @char != '"') { }
        public override string ReadExpression(TextReader reader)
        {
            base.SkipOneChar(reader);
            string result = base.ReadWhile(reader, base._readCondition);
            base.SkipOneChar(reader);
            return result;
        }
    }
}
