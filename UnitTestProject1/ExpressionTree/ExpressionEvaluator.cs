using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree
{
    public class ExpressionEvaluator
    {
        private readonly Stack<Expression> _expressionStack = new Stack<Expression>();
        private readonly Stack<ExpressionOperation> _operationStack = new Stack<ExpressionOperation>();

        public Func<Dictionary<string, string>, bool> Build(string expression)
        {
            var dictionaryParameter = Expression.Parameter(typeof(Dictionary<string, string>), "args");

            using (var reader = new StringReader(expression))
            {
                int peek;
                while((peek = reader.Peek()) > -1)
                {
                    var next = (char)peek;
                    if (char.IsDigit(next))
                    {
                        _expressionStack.Push(ReadOperand(reader));
                    }
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
                    else
                    {
                        string operation = new char[] { '&', '|', '>', '=', '<', '|' }.Contains(next)
                            ? ((char)reader.Read()).ToString() + ((char)reader.Read()).ToString()
                            : ((char)reader.Read()).ToString();

                        var currentOperation = (ExpressionOperation)operation;
                        EvaluateWhile(() => _operationStack.Count > 0 && currentOperation.Precedence >= _operationStack.Peek().Precedence);

                        _operationStack.Push(currentOperation);
                    }
                }
            }

            EvaluateWhile(() => _operationStack.Count > 0);

            var lambda = Expression.Lambda<Func<Dictionary<string, string>, bool>>(_expressionStack.Pop(), dictionaryParameter);
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
