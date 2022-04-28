using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shimi
{
    internal static class ShimCollection<TResult>
    {
        private static readonly List<Shim<TResult>> _shims = new List<Shim<TResult>>();

        public static void Add(Shim<TResult> shim)
        {
            lock (_shims)
            {
                _shims.Remove(shim);
                _shims.Add(shim);
            }
        }

        public static void Remove(Shim<TResult> shim)
        {
            lock (_shims)
            {
                _shims.Remove(shim);
            }
        }

        public static string GetIDOf(MethodBase methodBase)
        {
            var shim = _shims.Find(f => f.Method == methodBase);
            if (shim == null)
                return Guid.NewGuid().ToString();

            return shim.ID;
        }

        public static bool Prefix(object __instance, object __originalMethod, ref TResult __result)
        {
            var shimsByTarget = _shims.Where(f => f.Target != null && f.Target.Equals(__instance)).ToArray();
            var shimsByMethod = _shims.Where(f => f.Method != null && f.Method.Equals(__originalMethod)).ToArray();

            var shims = shimsByTarget.Concat(shimsByMethod).Distinct().ToArray();
            
            var shim = shims.FirstOrDefault();
            if (shim == null)
                return true;

            __result = shim.Result;
            return false;
        }
    }
}
