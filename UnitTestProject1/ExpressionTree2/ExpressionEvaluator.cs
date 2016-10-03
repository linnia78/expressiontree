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
            new NumericParser(new NumericReader()),
            new QuotationParser(new QuotationReader()),
            new BlankParser(new BlankReader())
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
                        var parsedExpression = parser.ParseExpression(reader);
                        if (parsedExpression != null) {
                            _expressionStack.Push(parsedExpression);
                        }
                    }
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
