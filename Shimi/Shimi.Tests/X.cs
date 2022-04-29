using System;
using System.Threading.Tasks;

namespace Shimi.Tests
{
    public interface IX
    {
        int InstanceMethod();
    }

    public class X : IX
    {
        public static readonly int StaticField = 0;
        public static int StaticProperty => 0;
        public static int StaticMethod() => 0;
        public static string FuncArg(int x) => string.Empty;

        public int InstanceProperty => 0;
        public int InstanceMethod() => 0;
        public Task<int> InstanceMethodAsync() => Task.FromResult(0);
        public Task<int> InstanceMethodWithFuncArgAsync(Func<int, string> function) => Task.FromResult(0);
        public int InstanceMethodWithArg(int x) => x;
    }

    public static class XExtensions
    {
        public static int ExtensionMethod(this X x) => 0;
        public static int ExtensionMethodWithFuncArg(this X x, Func<int, string> function) => 0;
    }
}
