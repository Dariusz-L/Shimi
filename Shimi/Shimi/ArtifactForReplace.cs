using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace Shimi
{
    public class ArtifactForReplace<TResult>
    {
        public object Target { get; }
        public MethodInfo Method { get; }

        public ArtifactForReplace(object target, MethodInfo method)
        {
            Target = target;
            Method = method;
        }

        public void To(TResult result, out Shim<TResult> shim)
        {
            string id = ShimCollection<TResult>.GetIDOf(Method);

            var originalMethods = Harmony.GetAllPatchedMethods();
            if (!originalMethods.Contains(Method))
            {
                var prefix = AccessTools.Method(typeof(ShimCollection<TResult>), nameof(ShimCollection<TResult>.Prefix));

                var harmony = new Harmony(id);
                harmony.Patch(Method, new HarmonyMethod(prefix));
            }

            shim = new Shim<TResult>(id, Target, Method, result);
            ShimCollection<TResult>.Add(shim);
        }

        public void To(TResult value) => To(value, out var shim);
    }
}
