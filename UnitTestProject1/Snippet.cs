using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class Snippet
    {
        public Func<object, object[], object> ChangeTypeSnippet(MethodInfo methodInfo)
        {
            var methodParams = methodInfo.GetParameters();
            var arrayParameter = Expression.Parameter(typeof(object[]), "array");

            var changeTypeMethod = typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(TypeCode) });
            var arguments =
                 methodParams.Select((p, i) =>
                     !typeof(IConvertible).IsAssignableFrom(p.ParameterType)
                         // If NOT IConvertible, don't try to convert it
                         ? (Expression)Expression.Convert(
                             Expression.ArrayAccess(arrayParameter, Expression.Constant(i)), p.ParameterType)
                        :
                        // Otherwise add an explicit conversion to the correct type to handle int <--> double etc.
                        (Expression)Expression.Convert(
                            Expression.Call(changeTypeMethod,
                                Expression.ArrayAccess(arrayParameter, Expression.Constant(i)),
                                Expression.Constant(Type.GetTypeCode(p.ParameterType))),
                            p.ParameterType)
                    )
                    .ToList();
            var instanceParameter = Expression.Parameter(typeof(object), "controller");

            var instanceExp = Expression.Convert(instanceParameter, methodInfo.DeclaringType);
            var callExpression = Expression.Call(instanceExp, methodInfo, arguments);

            var bodyExpression = Expression.Convert(callExpression, typeof(object));

            return Expression.Lambda<Func<object, object[], object>>(
                bodyExpression, instanceParameter, arrayParameter)
                .Compile();
        }
    }
}
