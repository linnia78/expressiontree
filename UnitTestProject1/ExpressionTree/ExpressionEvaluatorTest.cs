using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject1.ExpressionTree
{
    [TestClass]
    public class ExpressionEvaluatorTest
    {
        [TestMethod]
        public void should_handle_logical_and()
        {
            var dictionary = new Dictionary<string, string> { { "a", "b" } };

            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("2+3>=1+7");
            var result = expression(dictionary);
        }
    }
}
