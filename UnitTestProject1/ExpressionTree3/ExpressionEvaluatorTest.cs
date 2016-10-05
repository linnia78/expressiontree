﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UnitTestProject1.ExpressionTree3
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

        [TestMethod]
        public void should_handle_dictionary_parameters()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var dictionary = new Dictionary<string, string> { { "word", "word" } };
            var expression = evaluator.Compile<Dictionary<string, string>>(@"""word"" == [word]");
            //Act
            var result = expression(dictionary);
            //Assert
            Assert.AreEqual(result, "word".Equals(dictionary["word"]));
        }

        [TestMethod]
        public void should_eval_complex_expression()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var dictionary = new Dictionary<string, string> { { "a", "20" }, { "b", null } };
            var expression = evaluator.Compile<Dictionary<string, string>>(@"""20"" == [a] && 20 > 5");
            var expression2 = evaluator.Compile<Dictionary<string, string>>("null == [b]");
            //Act
            var result = expression(dictionary);
            var result2 = expression2(dictionary);
            //Assert
            Assert.AreEqual(result, "20".Equals(dictionary["a"]) && 20 > 5);
            Assert.AreEqual(result2, dictionary["b"] == null);
        }

        [TestMethod]
        public void should_eval_complex_type()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var dictionary = new Dictionary<string, object> { { "a", 20m }, { "b", "100" } };
            var expression = evaluator.Compile<Dictionary<string, object>>("20 > [a]");
            var expression2 = evaluator.Compile<Dictionary<string, object>>("20 > [b]");
            //Act
            var result = expression(dictionary);
            var result2 = expression(dictionary);
            //Assert
            Assert.AreEqual(result, 20 > (decimal)dictionary["a"]);
            Assert.AreEqual(result2, 20 > Convert.ToDecimal(dictionary["b"]));
        }

        [TestMethod]
        public void should_eval_paranthesis()
        {
            //Arrange
            var evaluator = new ExpressionEvaluator();
            var dictionary = new Dictionary<string, object> { { "a", 20m }, { "b", "100" } };
            var expression = evaluator.Compile<Dictionary<string, object>>("(1 + 2) * 3 > 10");
            var expression2 = evaluator.Compile<Dictionary<string, object>>("(1 + [b]) * 2 > 100");
            //Act
            var result = expression(dictionary);
            var result2 = expression2(dictionary);
            //Assert
            Assert.AreEqual(result, (1 + 2) * 3 > 10);
            Assert.AreEqual(result2, (1 + Convert.ToDecimal(dictionary["b"])) * 2 > 100);
        }

        [TestMethod]
        public void test()
        {
            var method = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) });
            var result = method.Invoke(null, new object[] { 20, typeof(decimal) });
        }

        [TestMethod]
        public void test2()
        {
            var expression = Expression.Call(null, typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) }), Expression.Constant("20", typeof(object)), Expression.Constant(typeof(decimal)));
            var lambda = Expression.Lambda<Func<object>>(expression);
            var result = lambda.Compile()();

            var expression2 = Expression.Call(null, typeof(Convert).GetMethod("ToString", new Type[] { typeof(int) }), Expression.Constant(20));
            var lambda2 = Expression.Lambda<Func<string>>(expression2);
            var result2 = lambda2.Compile()();
        }

        [TestMethod]
        public void test3()
        {
            var param = Expression.Parameter(typeof(Person), "person");
            var property = typeof(Person).GetProperty("Value");
            var method = typeof(Convert).GetMethod("ToDecimal", new Type[] { typeof(object) });
            var value = Expression.Call(null, method, Expression.Property(param, property));

            var expression = Expression.GreaterThan(value, Expression.Constant(100m));
            var lambda = Expression.Lambda<Func<Person, bool>>(expression, param);
            var compiled = lambda.Compile();
            var result = compiled(new Person { Value = "20" });
            var result2 = compiled(new Person { Value = "1000" });
        }

        public class Person
        {
            public string Value { get; set; }
        }
    }
}
