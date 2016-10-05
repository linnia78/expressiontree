using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree3
{
    public class ReservedWordReader : StatementReader
    {
        public ReservedWordReader() : base(char.IsLetter) { }
    }
}
