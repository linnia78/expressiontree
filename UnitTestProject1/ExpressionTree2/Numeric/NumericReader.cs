using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class NumericReader : ExpressionReader
    {
        public NumericReader() : base((@char) => char.IsDigit(@char) || @char == '.') { }
    }
}
