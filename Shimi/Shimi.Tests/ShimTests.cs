using NUnit.Framework;
using System;
using System.IO;

namespace Shimi.Tests
{
    public class ShimTests
    {
        [Test]
        public void GivenNoShimsBefore_WhenModifiedDirectoryGetFiles_ThenCorrect()
        {
            Shim.ResultOf(() => Directory.GetFiles(P.Any<string>())).To(new string[] { "X" }, out var shim);
            Assert.IsTrue(Directory.GetFiles("Random string")[0] == "X");
            Shim.Clear(shim);
            Assert.Throws<DirectoryNotFoundException>(() => Directory.GetFiles("Random string"));
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedDateTimeNow_ThenCorrect()
        {
            Shim.ResultOf(() => DateTime.Now).To(DateTime.MinValue, out var shim);
            Assert.AreEqual(DateTime.MinValue, DateTime.Now);
            Shim.Clear(shim);
            Assert.AreNotEqual(DateTime.MinValue, DateTime.Now);
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedInstanceMethodWithArg_ThenCorrect()
        {
            var x = new X();

            Assert.AreEqual(5, x.InstanceMethodWithArg(5));
            Shim.ResultOf(() => x.InstanceMethodWithArg(5)).To(10);
            Assert.AreEqual(10, x.InstanceMethodWithArg(5));
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedInstanceMethod_ThenCorrect()
        {
            var x = new X();

            Assert.AreEqual(0, x.InstanceMethod());
            Shim.ResultOf(() => x.InstanceMethod()).To(10);
            Assert.AreEqual(10, x.InstanceMethod());
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedInstanceProperty_ThenCorrect()
        {
            var x = new X();

            Assert.AreEqual(0, x.InstanceProperty);
            Shim.ResultOf(() => x.InstanceProperty).To(10);
            Assert.AreEqual(10, x.InstanceProperty);
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedStaticProperty_ThenCorrect()
        {
            Assert.AreEqual(0, X.StaticProperty);
            Shim.ResultOf(() => X.StaticProperty).To(10, out var shim);
            Assert.AreEqual(10, X.StaticProperty);
            Shim.Clear(shim);
        }

        [Test]
        public void GivenNoShimsBefore_WhenModifiedStaticMethod_ThenCorrect()
        {
            Assert.AreEqual(0, X.StaticMethod());
            Shim.ResultOf(() => X.StaticMethod()).To(7, out var shim);
            Assert.AreEqual(7, X.StaticMethod());
            Shim.Clear(shim);
        }

        [Test]
        public void GivenNoShimsBefore_WhenCreatedShimAndCleared_ThenModifiedMethodReset()
        {
            Assert.AreEqual(0, X.StaticMethod());
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

        public int InstanceProperty => 0;
        public int InstanceMethod() => 0;
        public int InstanceMethodWithArg(int x) => x;
    }
}
