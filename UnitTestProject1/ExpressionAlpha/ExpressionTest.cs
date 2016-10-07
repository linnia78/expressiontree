using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.ExpressionAlpha
{
    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new ParameterContainer
            {
                { "Hello", new ParameterItem { Instance = null, Type = typeof(object) } }
            };
        }
    }
}
