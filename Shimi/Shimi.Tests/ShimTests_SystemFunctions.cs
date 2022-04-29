using NUnit.Framework;
using System;
using System.IO;

namespace Shimi.Tests
{
    public class ShimTests_SystemFunctions
    {
        [Test]
        public void Replace_DirectoryGetFiles()
        {
            Shim.ResultOf(() => Directory.GetFiles(P.Any<string>())).To(new string[] { "X" }, out var shim);
            Assert.IsTrue(Directory.GetFiles("Random string")[0] == "X");
            Shim.Clear(shim);
            Assert.Throws<DirectoryNotFoundException>(() => Directory.GetFiles("Random path"));
        }

        [Test]
        public void Replace_DateTimeNow()
        {
            Shim.ResultOf(() => DateTime.Now).To(DateTime.MinValue, out var shim);
            Assert.AreEqual(DateTime.MinValue, DateTime.Now);
            Shim.Clear(shim);
            Assert.AreNotEqual(DateTime.MinValue, DateTime.Now);
        }
    }
}
