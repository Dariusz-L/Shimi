using System;
using System.Linq.Expressions;

namespace Shimi
{
    public static partial class Shim
    {
        public static ArtifactForReplace<TResult> ResultOf<TResult>(Expression<Func<TResult>> expression)
        {
            if (expression.Body is MemberExpression memberExp)
            {
               var target = MemberExpressionToTargetProvider.GetTargetFrom(memberExp);
               var method = MemberExpressionToTargetProvider.GetMethodFrom(memberExp);

               return new ArtifactForReplace<TResult>(target, method);
            }

            if (expression.Body is MethodCallExpression methodCallExp)
            {
                var target = MethodExpressionToTargetProvider.GetTargetFrom(methodCallExp);
                var method = MethodExpressionToTargetProvider.GetMethodFrom(methodCallExp);

                return new ArtifactForReplace<TResult>(target, method);
            }

            return null;
        }
    }
}
