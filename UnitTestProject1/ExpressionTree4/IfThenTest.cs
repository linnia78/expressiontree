using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace UnitTestProject1.ExpressionTree4
{
    [TestClass]
    public class IfThenTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var expression = "if (3 > 2) { Helper.Log(); }";
            //Expression.IfThen(Expression.GreaterThan(Expression.Constant(3), Expression.Constant(2)))
        }
    }
}
