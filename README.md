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
  Just take a look at tests written in [here](Shimi/Shimi.Tests/ShimTests.cs).
  Example:
  ```C#
  [Test]
  public void GivenNoShimsBefore_WhenModifiedDateTimeNow_ThenCorrect() // still learning to better name tests
  {
      Shim.ResultOf(() => DateTime.Now).To(DateTime.MinValue, out var shim);
      Assert.AreEqual(DateTime.MinValue, DateTime.Now);
      Shim.Clear(shim);
      Assert.AreNotEqual(DateTime.MinValue, DateTime.Now);
  }
  ```
