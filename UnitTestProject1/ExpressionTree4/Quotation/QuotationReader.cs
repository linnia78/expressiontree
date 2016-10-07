using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree4
{
    public class QuotationReader : StatementReader
    {
        public QuotationReader() : base((@char) => @char != '"') { }
        public override string ReadStatement(TextReader textReader)
        {
            base.SkipOneChar(textReader);
            string result = base.ReadWhile(textReader, base._readCondition);
            base.SkipOneChar(textReader);
            return result;
        }
    }
}
