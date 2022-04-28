namespace Shimi
{
    public static partial class Shim
    {
        public static void Clear<T0>(Shim<T0> _0) => _0.Clear();

        public static void Clear<T0, T1>(Shim<T0> _0, Shim<T1> _1)
        { _0.Clear(); _1.Clear(); }

        public static void Clear<T0, T1, T2>(Shim<T0> _0, Shim<T1> _1, Shim<T2> _2)
        { _0.Clear(); _1.Clear(); _2.Clear(); }

        public static void Clear<T0, T1, T2, T3>(Shim<T0> _0, Shim<T1> _1, Shim<T2> _2, Shim<T3> _3)
        { _0.Clear(); _1.Clear(); _2.Clear(); _3.Clear(); }

        public static void Clear<T0, T1, T2, T3, T4>(Shim<T0> _0, Shim<T1> _1, Shim<T2> _2, Shim<T3> _3, Shim<T4> _4)
        { _0.Clear(); _1.Clear(); _2.Clear(); _3.Clear(); _4.Clear(); }
    }
}
