using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // The expression tree to execute.
            BinaryExpression be = Expression.Power(Expression.Constant(2D), Expression.Constant(3D));

            // Create a lambda expression.
            Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);

            // Compile the lambda expression.
            Func<double> compiledExpression = le.Compile();

            // Execute the lambda expression.
            double result = compiledExpression();

            // This code produces the following output:
            // 8
        }

        [TestMethod]
        public void TestParameters()
        {
            var parameters = new List<string> { "no", "man" }.ToArray();
            var parameters2 = new List<string> { "blah", "yo" }.ToArray();
            var param = Expression.Parameter(typeof(string[]));
            var constant = Expression.Constant("yo");
            var expression = Expression.OrElse(
                Expression.Equal(Expression.ArrayIndex(param, Expression.Constant(0)), constant), 
                Expression.Equal(Expression.ArrayIndex(param, Expression.Constant(1)), constant));

            var lambda = Expression.Lambda<Func<string[], bool>>(expression, new ParameterExpression[] { param });
            var compiled = lambda.Compile();

            var result = compiled(parameters);
            var result2 = compiled(parameters2);
        }

        [TestMethod]
        public void TestDictionaryParameters()
        {
            var param = Expression.Parameter(typeof(Dictionary<string, string>), "args");
            var key = Expression.Parameter(typeof(string), "key");
            var result = Expression.Parameter(typeof(string), "result");
            var block = Expression.Block(new[] { result }, Expression.Assign(result, Expression.Property(param, "Item", key)),
                result);
            var lambda = Expression.Lambda<Func<Dictionary<string, string>, string, object>>(block, param, key).Compile();

            var testParam = new Dictionary<string, string> { { "test", "test" }, { "other", "ya" } };
            var response = lambda(testParam, "test");
            var response2 = lambda(testParam, "other");
            
        }

        [TestMethod]
        public void BasicAndTest()
        {
            var dictionary = new Dictionary<string, string> { { "a", "b" } };

            var evaluator = new ExpressionEvaluator();
            var expression = evaluator.Build("a=\"b\"&a=\"b\"");
            var result = expression(dictionary);
        }
    }

    public class ExpressionEvaluator
    {
        private readonly Stack<Expression> _expressionStack = new Stack<Expression>();
        private readonly Stack<Func<Expression, Expression, Expression>> _operatorStack = new Stack<Func<Expression, Expression, Expression>>();
        private readonly List<string> _parameters = new List<string>();
        public ExpressionEvaluator()
        {

        }

        public Func<Dictionary<string, string>, bool> Build(string expression)
        {
            var dictionary = Expression.Parameter(typeof(Dictionary<string, string>), "args");


            using (var reader = new StringReader(expression))
            {
                int peek;
                while((peek = reader.Peek()) > -1)
                {
                    char next = (char)peek;
                    if (next == '&')
                    {
                        reader.Read();
                        EvaluateWhile(() => _operatorStack.Count > 0);
                        _operatorStack.Push(Expression.AndAlso);
                    }
                    else if (next == '=')
                    {
                        reader.Read();
                        _operatorStack.Push(Expression.Equal);
                    }
                    else if (next == '"')
                    {
                        reader.Read();
                        _expressionStack.Push(Expression.Constant(Convert.ToString((char)reader.Read())));
                        reader.Read();
                    }
                    else
                    {
                        //return Expression.Lambda<Func<Dictionary<string, string>, string>>(ParseOperands(reader, dictionary), dictionary).Compile();
                        _expressionStack.Push(ParseOperands(reader, dictionary));
                    }
                }
            }

            EvaluateWhile(() => _operatorStack.Count > 0);

            return Expression.Lambda<Func<Dictionary<string, string>, bool>>(_expressionStack.Pop(), dictionary).Compile();
        }

        private void EvaluateWhile(Func<bool> condition)
        {
            while (condition())
            {
                var operation = _operatorStack.Pop();

                _expressionStack.Push(operation(_expressionStack.Pop(), _expressionStack.Pop()));
            }
        }

        public Expression ParseOperands(TextReader reader, Expression dictionary)
        {
            int peek;
            string param = string.Empty;
            while((peek = reader.Peek()) > -1)
            {
                var next = (char)peek;
                if (char.IsLetter(next))
                {
                    reader.Read();
                    param += next;
                }
                else
                {
                    break;
                }
            }

            var key = Expression.Constant(param);
            var value = Expression.Parameter(typeof(string), "value");
            var block = Expression.Block(new[] { value }, Expression.Assign(value, Expression.Property(dictionary, "Item", key)), value);

            return block;
        }
        //public Func<bool> Evaluate(string expression)
        //{
        //    var parts = expression.Split(new string[] { "&&" }, StringSplitOptions.None);
        //    var condition = parts.Aggregate(Expression.Constant(true) as Expression, (current, next) =>
        //    {
        //        return Expression.And(current, Expression.Constant(bool.Parse(next)));
        //    });
        //    var lambda = Expression.Lambda<Func<bool>>(condition);

        //    return lambda.Compile();
        //}

    }
    
}
