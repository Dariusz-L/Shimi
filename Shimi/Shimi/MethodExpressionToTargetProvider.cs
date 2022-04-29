using System.Linq.Expressions;
using System.Reflection;

namespace Shimi
{
    internal class MethodExpressionToTargetProvider
    {
        public static object GetTargetFrom(MethodCallExpression expression)
        {
            var fex = expression.Object as MemberExpression;
            if (fex == null)
                return null;

            var cex = fex.Expression as ConstantExpression;
            var fld = fex.Member as FieldInfo;

            return fld.GetValue(cex.Value);
        }

        public static MethodInfo GetMethodFrom(MethodCallExpression expression)
        {
            var target = GetTargetFrom(expression);
            if (target == null)
                return expression.Method;

            var targetType = target.GetType();
            return targetType.GetMethod(expression.Method.Name);
        }
    }
}
