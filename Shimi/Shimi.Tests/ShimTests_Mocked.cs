using Moq;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Shimi.Tests
{
    public class ShimTests_Mock
    {
        [Test]
        public void ReplaceNot_NSubstituteMock()
        {
            var x = Substitute.For<IX>();
            x.InstanceMethod().Returns(15);

            Action action = () => Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.Throws<Exception>(() => action());
        }

        [Test]
        public void ReplaceNot_MoqMock()
        {
            var x = new Mock<IX>();
            x.Setup(x => x.InstanceMethod()).Returns(10);
            var @object = x.Object;

            Action action = () => Shim.ResultOf(() => @object.InstanceMethod()).To(10);
            Assert.Throws<Exception>(() => action());
        }
    }
}
