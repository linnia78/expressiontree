using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ExpressionTree4.Conditional
{
    public class Conditional
    {
        private readonly Func<Expression, Expression, Expression> _ifThen;
        private readonly Func<Expression, Expression, Expression, Expression> _ifThenElse;
        public static readonly Conditional IfThen = new Conditional(Expression.IfThen);
        public static readonly Conditional IfThenElse = new Conditional(Expression.IfThenElse);

        public Conditional(Func<Expression, Expression, Expression> fn)
        {
            this._ifThen = fn;
        }

        public Conditional(Func<Expression, Expression, Expression, Expression> fn)
        {
            this._ifThenElse = fn;
        }
    }
}
