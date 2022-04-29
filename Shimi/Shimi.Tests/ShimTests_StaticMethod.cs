using NUnit.Framework;

namespace Shimi.Tests
{
    public class ShimTests_StaticMethod
    {
        [Test]
        public void Replace()
        {
            Shim.ResultOf(() => X.StaticMethod()).To(7, out var shim);
            Assert.AreEqual(7, X.StaticMethod());
            Shim.Clear(shim);
        }

        [Test]
        public void Clear()
        {
            Shim.ResultOf(() => X.StaticMethod()).To(7, out var shim);
            Assert.AreEqual(7, X.StaticMethod());
            Shim.Clear(shim);
            Assert.AreEqual(0, X.StaticMethod());
        }
    }
}
