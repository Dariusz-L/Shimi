using NUnit.Framework;
using System.Threading.Tasks;

namespace Shimi.Tests
{
    public class ShimTests_InstanceMethod
    {
        [Test]
        public void Replace()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }

        [Test]
        public void Replace_OfInterface()
        {
            IX x = new X();

            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }

        [Test]
        public void Replace_WithArg()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethodWithArg(5)).To(10);
            Assert.AreEqual(10, x.InstanceMethodWithArg(5));
        }

        [Test]
        public async Task Replace_WithFuncArgAsync()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethodWithFuncArgAsync(X.FuncArg)).To(Task.FromResult(10));
            Assert.AreEqual(10, await x.InstanceMethodWithFuncArgAsync(X.FuncArg));
        }

        [Test]
        public async Task Replace_WithFuncArgAsync_Null()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethodWithFuncArgAsync(null)).To(Task.FromResult(10));
            Assert.AreEqual(10, await x.InstanceMethodWithFuncArgAsync(null));
        }
    }
}
