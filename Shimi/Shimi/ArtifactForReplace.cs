using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace Shimi
{
    public class ArtifactForReplace<TResult>
    {
        public object Target { get; }
        public MethodInfo MethodInfo { get; }

        public ArtifactForReplace(object target, MethodInfo methodInfo)
        {
            Target = target;
            MethodInfo = methodInfo;
        }

        public void To(TResult value, out Shim<TResult> shim)
        {
            string id = ShimCollection<TResult>.GetIDOf(MethodInfo);

            var originalMethods = Harmony.GetAllPatchedMethods();
            if (!originalMethods.Contains(MethodInfo))
            {
                var prefix = AccessTools.Method(typeof(ShimCollection<TResult>), nameof(ShimCollection<TResult>.Prefix));

                var harmony = new Harmony(id);
                harmony.Patch(MethodInfo, new HarmonyMethod(prefix));
            }

            shim = new Shim<TResult>(id, Target, MethodInfo, value);
            ShimCollection<TResult>.Add(shim);
        }

        public void To(TResult value) => To(value, out var fake);
    }
}
