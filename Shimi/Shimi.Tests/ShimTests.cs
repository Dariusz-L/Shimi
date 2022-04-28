using NUnit.Framework;
using System;
using System.IO;

namespace Shimi.Tests
{
    public class ShimTests
    {
        [Test]
        public void ShouldReplaceExtensionMethodWithFuncArg_Lambda()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(x => string.Empty));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, x => string.Empty));
        }

        //[Test]
        public void ShouldReplaceExtensionMethodWithFuncArg_Any()
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
        public void ShouldReplaceExtensionMethodWithFuncArg_StaticMethod()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(X.FuncArg));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, X.FuncArg));
        }

        [Test]
        public void ShouldReplaceExtensionMethodWithFuncArg_Null()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethodWithFuncArg(P.Any<Func<int, string>>())).To(10);
            Assert.AreEqual(10, x.ExtensionMethodWithFuncArg(null));

            Shim.ResultOf(() => XExtensions.ExtensionMethodWithFuncArg(P.Any<X>(), P.Any<Func<int, string>>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethodWithFuncArg(x, null));
        }

        [Test]
        public void ShouldReplaceExtensionMethod()
        {
            var x = new X();

            Shim.ResultOf(() => x.ExtensionMethod()).To(10);
            Assert.AreEqual(10, x.ExtensionMethod());

            Shim.ResultOf(() => XExtensions.ExtensionMethod(P.Any<X>())).To(15);
            Assert.AreEqual(15, XExtensions.ExtensionMethod(x));
        }

        [Test]
        public void ShouldReplaceDirectoryGetFiles()
        {
            Shim.ResultOf(() => Directory.GetFiles(P.Any<string>())).To(new string[] { "X" }, out var shim);
            Assert.IsTrue(Directory.GetFiles("Random string")[0] == "X");
            Shim.Clear(shim);
            Assert.Throws<DirectoryNotFoundException>(() => Directory.GetFiles("Random path"));
        }

        [Test]
        public void ShouldReplaceDateTimeNow()
        {
            Shim.ResultOf(() => DateTime.Now).To(DateTime.MinValue, out var shim);
            Assert.AreEqual(DateTime.MinValue, DateTime.Now);
            Shim.Clear(shim);
            Assert.AreNotEqual(DateTime.MinValue, DateTime.Now);
        }

        [Test]
        public void ShouldReplaceInstanceMethodWithArg()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethodWithArg(5)).To(10);
            Assert.AreEqual(10, x.InstanceMethodWithArg(5));
        }

        [Test]
        public void ShouldReplaceInstanceMethod()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }

        [Test]
        public void ShouldReplaceInstanceProperty()
        {
            var x = new X();

            Shim.ResultOf(() => x.InstanceProperty).To(10);
            Assert.AreEqual(10, x.InstanceProperty);
        }

        [Test]
        public void ShouldReplaceStaticProperty()
        {
            Shim.ResultOf(() => X.StaticProperty).To(10, out var shim);
            Assert.AreEqual(10, X.StaticProperty);
            Shim.Clear(shim);
        }

        [Test]
        public void ShouldReplaceStaticMethod()
        {
            Shim.ResultOf(() => X.StaticMethod()).To(7, out var shim);
            Assert.AreEqual(7, X.StaticMethod());
            Shim.Clear(shim);
        }

        [Test]
        public void ShouldClearStaticMethod()
        {
            Shim.ResultOf(() => X.StaticMethod()).To(7, out var shim);
            Assert.AreEqual(7, X.StaticMethod());
            Shim.Clear(shim);
            Assert.AreEqual(0, X.StaticMethod());
        }
    }

    public class X
    {
        public static readonly int StaticField = 0;
        public static int StaticProperty => 0;
        public static int StaticMethod() => 0;
        public static string FuncArg(int x) => string.Empty;

        public int InstanceProperty => 0;
        public int InstanceMethod() => 0;
        public int InstanceMethodWithArg(int x) => x;
    }

    public static class XExtensions
    {
        public static int ExtensionMethod(this X x) => 0;
        public static int ExtensionMethodWithFuncArg(this X x, Func<int, string> function) => 0;
    }
}
