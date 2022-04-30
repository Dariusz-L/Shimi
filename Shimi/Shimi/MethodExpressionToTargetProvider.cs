using HarmonyLib;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            //if (target == null)
            {
                var expMethod = expression.Method;
                var declaringType = target?.GetType() ?? expMethod.DeclaringType;

                //return expMethod;
                var parameterTypes = expMethod.GetParameters().ToArray();
                var genericTypes = expMethod.GetGenericArguments();
                var methods = declaringType.GetMethods();
                var methodsOfName = methods.Where(m => m.Name == expMethod.Name).ToArray();
                var methodsWithSameMetadata = methodsOfName.Where(
                    m =>
                    {
                        var mParams = m.GetParameters();
                        if (mParams.Length != parameterTypes.Length)
                            return false;

                        var sameParams = mParams.Zip(parameterTypes, (x, y) => new { x, y })
                            .All(p =>
                            {
                                var isSameType = p.x.ParameterType.Equals(p.y.ParameterType);
                                var isSameOut = p.x.IsOut.Equals(p.y.IsOut);
                                var isSameIn = p.x.IsIn.Equals(p.y.IsIn);
                                var isSameIsRetval = p.x.IsRetval.Equals(p.y.IsRetval);
                                var isSameName = p.x.Name.Equals(p.y.Name);

                                return isSameType && isSameOut && isSameIn && isSameIsRetval && isSameName;
                            });

                        return sameParams;
                    })
                .ToArray();

                var method = methodsWithSameMetadata.FirstOrDefault();
                if (method == null)
                {
                    var m = methodsOfName.Select(m => m.MakeGenericMethod(genericTypes)).Where(m => m != null).FirstOrDefault();
                    return m;
                }
                if (method.IsGenericMethod)
                    return method.MakeGenericMethod(genericTypes);

                return method;
            }

            var targetType = target.GetType();
            return targetType.GetMethod(expression.Method.Name);
        }
    }
}
