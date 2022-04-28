using System.Reflection;

namespace Shimi
{
    public class Shim<TResult>
    {
        public string ID { get; }
        public object Target { get; }
        public MethodBase Method { get; }
        public TResult Result { get; }

        public Shim(string id, object target, MethodBase method, TResult result)
        {
            ID = id;
            Target = target;
            Method = method;
            Result = result;
        }

        public void Clear() => ShimCollection<TResult>.Remove(this);

        public override bool Equals(object other)
        {
            var otherShim = other as Shim<TResult>;
            if (otherShim == null)
                return base.Equals(other);

            if (otherShim.Target != Target)
                return false;

            if (otherShim.Method != Method)
                return false;

            return true;
        }
    }
}
