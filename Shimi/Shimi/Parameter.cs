namespace Shimi
{
    public static class P
    {
        public static P<T> Any<T>()
        {
            return new P<T>();
        }
    }

    public class P<T>
    {
        public static implicit operator T(P<T> a) => a;
    }
}
