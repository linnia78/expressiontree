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
            var result = expression();
            var result2 = expression2();
            var result3 = expression3();
            //Assert
            Assert.AreEqual(result, true && true);
            Assert.AreEqual(result2, true && false);
            Assert.AreEqual(result3, false && false);
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
            var result = expression();
            var result2 = expression2();
            var result3 = expression3();
            //Assert
            Assert.AreEqual(result, true || true);
            Assert.AreEqual(result2, true || false);
            Assert.AreEqual(result3, false || false);
        }

        [TestMethod]
        public void should_evaluate_complex_logical_and_or()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("true && false || true");
            var expression2 = evaluator.Build("true || false && true");
            var expression3 = evaluator.Build("true || false || true && true && false");
            //Act
            var result = expression();
            var result2 = expression2();
            var result3 = expression3();
            //Assert
            Assert.AreEqual(result, true && false || true);
            Assert.AreEqual(result2, true || false && true);
            Assert.AreEqual(result3, true || false || true && true && false);
        }

        [TestMethod]
        public void should_evaulate_singular_boolean_condition()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("true");
            var expression2 = evaluator.Build("false");
            //Act
            var result = expression();
            var result2 = expression2();
            //Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(result2, false);
        }

        [TestMethod]
        public void should_evaulate_string_equivalence_comparision()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build(@"""word"" == ""word""");
            var expression2 = evaluator.Build(@"""word"" != ""word""");
            var expression3 = evaluator.Build(@"""word"" == ""word2""");
            var expression4 = evaluator.Build(@"""word"" != ""word2""");
            //Act
            var result = expression();
            var result2 = expression2();
            var result3 = expression3();
            var result4 = expression4();
            //Assert
            Assert.AreEqual(result, "word".Equals("word"));
            Assert.AreEqual(result2, !"word".Equals("word"));
            Assert.AreEqual(result3, "word".Equals("word2"));
            Assert.AreEqual(result4, !"word".Equals("word2"));
        }
    }
}
