using System.Linq.Expressions;
using System.Reflection;

namespace Shimi
{
    internal class MemberExpressionToTargetProvider
    {
        public static object GetTargetFrom(MemberExpression expression)
        {
            var fex = expression.Expression as MemberExpression;
            if (fex == null)
                return null;

            var cex = fex.Expression as ConstantExpression;
            var fld = fex.Member as FieldInfo;

            return fld.GetValue(cex.Value);
        }

        public static MethodInfo GetMethodFrom(MemberExpression expression)
        {
            var propertyInfo = expression.Member as PropertyInfo;
            if (propertyInfo == null)
                return null;

            return propertyInfo.GetMethod;
        }
    }
}
