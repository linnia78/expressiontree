using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class ExpressionEvaluator
    {
        private readonly Stack<Expression> _expressionStack = new Stack<Expression>();
        private readonly Stack<Operation> _operationStack = new Stack<Operation>();

        private readonly IEnumerable<IExpressionParser> _parsers = new IExpressionParser[]
        {
            new ReservedWordParser(new ReservedWordReader()),
            new NumericParser(new NumericReader())
        };

        public Func<bool> Build(string expression)
        {
            //var dictionaryParameter = Expression.Parameter(typeof(T), "args");

            using (var reader = new StringReader(expression))
            {
                int peek;
                while((peek = reader.Peek()) > -1)
                {
                    var next = (char)peek;

                    var parser = _parsers.FirstOrDefault(x => x.CanParse(next));
                    if (parser != null)
                    {
                        _expressionStack.Push(parser.ReadExpression(reader));
                    }
                    //else if (char.IsDigit(next))
                    //{
                    //    _expressionStack.Push(ReadOperand(reader));
                    //}
                    else if (next == '"')
                    {
                        reader.Read();
                        _expressionStack.Push(Expression.Constant(Convert.ToString((char)reader.Read())));
                        reader.Read();
                    }
                    else if (next == ' ')
                    {
                        reader.Read();
                    }
                    //else if (char.IsLetter(next))
                    //{
                    //    var symbol = string.Empty;
                    //    do
                    //    {
                    //        symbol += ((char)reader.Read()).ToString();
                    //    }
                    //    while ((peek = reader.Peek()) > -1 && char.IsLetter((char)peek));

                    //    if (ReservedWordParser.IsReservedWord(symbol))
                    //    {
                    //        _expressionStack.Push(ReservedWordParser.GetExpression(symbol));
                    //    }

                    //}
                    else if (OperationReader.IsOperation(reader))
                    {
                        var currentOperation = OperationReader.GetOperation(reader);
                        EvaluateWhile(() => _operationStack.Count > 0 && currentOperation.Precedence >= _operationStack.Peek().Precedence);

                        _operationStack.Push(currentOperation);
                    }
                    else
                    {
                        throw new Exception("critical error");
                    }
                }
            }

            EvaluateWhile(() => _operationStack.Count > 0);

            //var lambda = Expression.Lambda<Func<T, bool>>(_expressionStack.Pop(), dictionaryParameter);
            var lambda = Expression.Lambda<Func<bool>>(_expressionStack.Pop());
            return lambda.Compile();
        }

        private Expression ReadOperand(TextReader reader)
        {
            var operand = string.Empty;

            int peek;

            while ((peek = reader.Peek()) > -1)
            {
                var next = (char)peek;

                if (char.IsDigit(next) || next == '.')
                {
                    reader.Read();
                    operand += next;
                }
                else
                {
                    break;
                }
            }

            return Expression.Constant(decimal.Parse(operand));
        }

        private void EvaluateWhile(Func<bool> condition)
        {
            while(condition())
            {
                var operation = _operationStack.Pop();

                var expressions = new Expression[operation.NumberOfOperands];
                for (var i = operation.NumberOfOperands - 1; i >= 0; i--)
                {
                    expressions[i] = _expressionStack.Pop();
                }
                _expressionStack.Push(operation.Apply(expressions));
            }
        }
    }
}
