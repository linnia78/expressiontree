using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public interface IExpressionParser
    {
        bool CanParse(char @char);
        Expression ReadExpression(TextReader reader);
    }
}
