using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class BlankReader : ExpressionReader
    {
        public BlankReader() : base(@char => @char == ' ') { }
    }
}
