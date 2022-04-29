using System;
using System.Linq.Expressions;

namespace Shimi
{
    public static partial class Shim
    {
        /// <summary>
        /// Evaluates expression and deduces an artifact to be replaced. This method does not affect the artifact directly,
        /// but creates an object which can be used further to replace actual return value of it.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">
        /// Lambda with artifact to be replaced.

        /// The lambda mustn't contain any input parameters, and the lambda expression must have format "target.Method(parameters)".
        /// Example: Shim.ResultOf(() => x.InstanceMethod());

        /// The target method might contain any parameters, it doesn't matter. You can use "P" class for it.
        /// Example: Shim.ResultOf(() => x.InstanceMethodWithArgs(P.Any<int>(), P.Any<string>())
        /// 
        /// </param>
        /// <returns>An object which can be directly used to replace a return value of the artifact.</returns>
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
