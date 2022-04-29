using NUnit.Framework;

namespace Shimi.Tests
{
    public class ShimTests_StaticProperty
    {
        [Test]
        public void Replace()
        {
            Shim.ResultOf(() => X.StaticProperty).To(10, out var shim);
            Assert.AreEqual(10, X.StaticProperty);
            Shim.Clear(shim);
        }
    }
}
