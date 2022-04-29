using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Shimi.Tests
{
    public class ShimTests_ExtensionMethod
    {
        [Test]
        public void Replace()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethod()).To(10);
            Assert.AreEqual(10, x.ExtensionMethod());

            Shim.ResultOf(() => XExtensions.ExtensionMethod(P.Any<X>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethod(x));
        }

        [Test]
        public void Replace_WithFuncArg_Lambda()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(x => string.Empty));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, x => string.Empty));
        }

        //[Test]
        public void Replace_WithFuncArg_Any()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            var a = P.Any<Func<int, string>>();
            try
            {
                var res = x.ExtensionMethodWithFuncArg(a);
                Assert.AreEqual(10, res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, X.FuncArg));
        }

        [Test]
        public void Replace_WithFuncArg_StaticMethod()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(X.FuncArg));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, X.FuncArg));
        }

        [Test]
        public void Replace_WithFuncArg_Null()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(null));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, null));
        }
    }
}
