using Moq;
using NSubstitute;
using NUnit.Framework;

namespace Shimi.Tests
{
    public class ShimTests_Mock
    {
        [Test]
        public void Replace_NSubstituteMock()
        {
            var x = Substitute.For<IX>();
            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }

        [Test]
        public void Replace_MoqMock()
        {
            var x = new Mock<IX>().Object;
            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }
    }
}
