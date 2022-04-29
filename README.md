# Shimi

  Shimi's reason of existence is to let you fake return values of any static or instance method or property, so you can test your code without unnecessarily introducing abstractions over your classes just for the sake of testability instead of good design.
  
  Popular testing libraries like Moq, NSubstitute do not give as much freedom in mocking anything you like. There are some, like Microsoft Fakes, TypeMock or JustMock, but they are huge and expensive. This is free alternative. It is built on top of [Harmony](https://github.com/pardeike/Harmony), which lets easily replace a code at runtime.

Please, treat it like a nice addition to your collection of traditional testing libraries. 

Check this out: [Stop overusing interfaces](https://blog.hovland.xyz/2017-04-22-stop-overusing-interfaces/).


## Installation
  There is a [Nuget](https://www.nuget.org/packages/Shimi/1.0.2) package, although initially not available for previous .Net versions.
  
  If you need you can just copy .cs files from [the project](/Shimi/Shimi) directory and import desired version of [Lib.Harmony](https://www.nuget.org/packages/Lib.Harmony/) to your project.
  
  In future I hope to add nuget packages for previous versions of .NET Framework or .Net Core, as well as Unity3D git package.
  
## How to use
  Just take a look at tests written in [here](Shimi/Shimi.Tests).
  Example:
  ```C#
  [Test]
  public void Replace_DateTimeNow()
  {
      // This will replace any call for DateTime.Now to DateTime.MinValue
      // The handle to this change resides in 'out var shim' variable, so you can revert the change later
      Shim.ResultOf(() => DateTime.Now).To(DateTime.MinValue, out var shim);
      Assert.AreEqual(DateTime.MinValue, DateTime.Now);
      
      // Remember to clear shim out, so it doesn't affect other code
      Shim.Clear(shim);
      Assert.AreNotEqual(DateTime.MinValue, DateTime.Now);
  }
  ```
  ### Syntax requirements
   Paramater of Shim.ResultOf method must be a lambda.
   - it mustn't contain any input parameters, and the lambda expression must have format "target.Method(parameters)".
   ```c#
   Shim.ResultOf(Shim.ResultOf(() => x.InstanceMethod()).To(10);
   ```
   - the target method might contain parameters, theyre values don't matter. You can use "P" class for it.
   ```c#
   Shim.ResultOf(() => x.InstanceMethodWithArgs(P.Any<int>(), P.Any<string>()).To(10);
   ```
  ### Limits
   - Any kind of expression, method, instance which is not covered by a test might not work.
	 Be sure to check [it](Shimi/Shimi.Tests).
	 They will be updated as time goes by to cover more cases.
