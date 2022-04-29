using NUnit.Framework;

namespace Shimi.Tests
{
    public class ShimTests_InstanceProperty
    {
        [Test]
        public void Replace()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceProperty).To(10);
            Assert.AreEqual(10, x.InstanceProperty);
        }
    }
}
