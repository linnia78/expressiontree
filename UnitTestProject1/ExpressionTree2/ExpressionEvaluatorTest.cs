using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject1.ExpressionTree2
{
    [TestClass]
    public class ExpressionEvaluatorTest
    {
        [TestMethod]
        public void should_handle_logical_and()
        {
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("2+3>=1+7");
            var result = expression();

            var expression2 = evaluator.Build("2+1<100");
            var result2 = expression2();
        }

        [TestMethod]
        public void should_evaluate_complex_logical_and()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("true && true");
            var expression2 = evaluator.Build("true && false");
            var expression3 = evaluator.Build("false && false");
            //Act
            var @truthy = expression();
            var @falsy = expression2();
            var @falsy2 = expression3();
            //Assert
            Assert.AreEqual(@truthy, true && true);
            Assert.AreEqual(@falsy, true && false);
            Assert.AreEqual(@falsy2, false && false);
        }

        [TestMethod]
        public void should_evaluate_complex_logical_or()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("true || true");
            var expression2 = evaluator.Build("true || false");
            var expression3 = evaluator.Build("false || false");
            //Act
            var @truthy = expression();
            var @truthy2 = expression2();
            var @falsy = expression3();
            //Assert
            Assert.AreEqual(@truthy, true || true);
            Assert.AreEqual(@truthy2, true || false);
            Assert.AreEqual(@falsy, false || false);
        }

        [TestMethod]
        public void should_evaluate_complex_logical_and_or()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();

        }

        [TestMethod]
        public void should_evaulate_singular_boolean_condition()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var truthyExpression = evaluator.Build("true");
            var falsyExpression = evaluator.Build("false");
            //Act
            var @true = truthyExpression();
            var @false = falsyExpression();
            //Assert
            Assert.AreEqual(@true, true);
            Assert.AreEqual(@false, false);
        }
    }
}
