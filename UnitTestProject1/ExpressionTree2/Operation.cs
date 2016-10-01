using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree2
{
    public class Operation
    {
        private readonly Func<Expression, Expression, Expression> _binaryOperation;
        private readonly Func<Expression, Expression> _unaryOperation;

        public int NumberOfOperands { get; private set; }
        public int Precedence { get; private set; } //precedence rules from C

        public static readonly Operation Negate = new Operation(2, Expression.Negate);
        public static readonly Operation Multiplication = new Operation(3, Expression.Multiply);
        public static readonly Operation Division = new Operation(3, Expression.Divide);
        public static readonly Operation Addition = new Operation(4, Expression.Add);
        public static readonly Operation Subtraction = new Operation(4, Expression.Subtract);
        public static readonly Operation LessThan = new Operation(6, Expression.LessThan);
        public static readonly Operation LessThanOrEqual = new Operation(6, Expression.LessThanOrEqual);
        public static readonly Operation GreaterThan = new Operation(6, Expression.GreaterThan);
        public static readonly Operation GreaterThanOrEqual = new Operation(6, Expression.GreaterThanOrEqual);
        public static readonly Operation EqualTo = new Operation(7, Expression.Equal);
        public static readonly Operation NotEqualTo = new Operation(7, Expression.NotEqual);
        public static readonly Operation LogicalAnd = new Operation(11, Expression.AndAlso);
        public static readonly Operation LogicalOr = new Operation(12, Expression.OrElse);


        public static readonly Dictionary<string, Operation> Operations = new Dictionary<string, Operation>
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

        private Operation(int precedence)
        {
            this.Precedence = precedence;
        }

        private Operation(int precedence, Func<Expression, Expression> unaryOperation) : this(precedence)
        {
            this._unaryOperation = unaryOperation;
            this.NumberOfOperands = 1;
        }

        private Operation(int precedence, Func<Expression, Expression, Expression> operation) : this(precedence)
        {
            this._binaryOperation = operation;
            this.NumberOfOperands = 2;
        }

        public Expression Apply(params Expression[] expressions)
        {
            return this.NumberOfOperands == 1
                ? _unaryOperation(expressions[0]) 
                : _binaryOperation(expressions[0], expressions[1]);
        }

        public static bool IsOperation(string operation)
        {
            return Operations.ContainsKey(operation);
        }

        public static explicit operator Operation(string operation)
        {
            Operation result;

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
