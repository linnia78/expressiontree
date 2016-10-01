using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree
{
    public class ExpressionOperation
    {
        private readonly Func<Expression, Expression, Expression> _operation;
        private readonly Func<Expression, Expression> _unaryOperation;

        public string Name { get; private set; }
        public int NumberOfOperands { get; private set; }
        public int Precedence { get; private set; } //precedence rules from C

        public static readonly ExpressionOperation Negate = new ExpressionOperation(2, Expression.Negate);
        public static readonly ExpressionOperation Multiplication = new ExpressionOperation(3, Expression.Multiply);
        public static readonly ExpressionOperation Division = new ExpressionOperation(3, Expression.Divide);
        public static readonly ExpressionOperation Addition = new ExpressionOperation(4, Expression.Add);
        public static readonly ExpressionOperation Subtraction = new ExpressionOperation(4, Expression.Subtract);
        public static readonly ExpressionOperation LessThan = new ExpressionOperation(6, Expression.LessThan);
        public static readonly ExpressionOperation LessThanOrEqual = new ExpressionOperation(6, Expression.LessThanOrEqual);
        public static readonly ExpressionOperation GreaterThan = new ExpressionOperation(6, Expression.GreaterThan);
        public static readonly ExpressionOperation GreaterThanOrEqual = new ExpressionOperation(6, Expression.GreaterThanOrEqual);
        public static readonly ExpressionOperation EqualTo = new ExpressionOperation(7, Expression.Equal);
        public static readonly ExpressionOperation NotEqualTo = new ExpressionOperation(7, Expression.NotEqual);
        public static readonly ExpressionOperation LogicalAnd = new ExpressionOperation(11, Expression.AndAlso);
        public static readonly ExpressionOperation LogicalOr = new ExpressionOperation(12, Expression.OrElse);


        private static readonly Dictionary<string, ExpressionOperation> Operations = new Dictionary<string, ExpressionOperation>
        {
            { "*", Multiplication },
            { "/", Division },
            { "+", Addition },
            { "-", Subtraction },
            { "<", LessThan },
            { "<=", LessThanOrEqual },
            { ">", GreaterThan },
            { ">=", GreaterThanOrEqual },
            { "==", EqualTo },
            { "!=", NotEqualTo },
            { "&&", LogicalAnd },
            { "||", LogicalOr }
        };

        private ExpressionOperation(int precedence)
        {
            this.Precedence = precedence;
        }

        private ExpressionOperation(int precedence, Func<Expression, Expression> unaryOperation) : this(precedence)
        {
            this._unaryOperation = unaryOperation;
            this.NumberOfOperands = 1;
        }

        private ExpressionOperation(int precedence, Func<Expression, Expression, Expression> operation) : this(precedence)
        {
            this._operation = operation;
            this.NumberOfOperands = 2;
        }

        public Expression Apply(params Expression[] expressions)
        {
            return _operation(expressions[0], expressions[1]);
        }

        public static bool IsOperation(string operation)
        {
            return Operations.ContainsKey(operation);
        }

        public static explicit operator ExpressionOperation(string operation)
        {
            ExpressionOperation result;

            if (Operations.TryGetValue(operation, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
