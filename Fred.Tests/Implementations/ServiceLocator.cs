using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Services;

namespace Fred.Tests;

[TestClass]
public class ServiceLocatorTests
{
    private interface ITestInterface {}
    
    private interface IWrongInterface {}

    private interface ISomeOtherInterface {}

    private class SomeOtherImplementation : ISomeOtherInterface {}
  
    private class TestImplementation: ITestInterface {}

    private class ComplexImplementation : ITestInterface
    {
        public ComplexImplementation()
        {}
        
        public ComplexImplementation(IWrongInterface wrongImplementation, ISomeOtherInterface someOtherImplementation)
        {           
            Assert.IsNotNull(wrongImplementation);
            Assert.IsNotNull(someOtherImplementation);
        }
    }
    
    private class WrongImplementation : IWrongInterface {}
    
    private class AnApiDefinition : IApiDefinition
    {
        public string Name => "Test";
        public string Description => "Testing";
        public Version Version => new("1");
        public string Root => "The Roots";
    }
    
    private class AnotherApiDefinition : IApiDefinition
    {
        public string Name => "Another Test";
        public string Description => "Testing";
        public Version Version => new("1");
        public string Root => "The Roots";
    }

    [TestMethod]
    public void CanOnlyRegisterInterfaces()
    {
        // These tests only exist because C# generics cannot be constrained to interfaces. 
        
        var implementation = new TestImplementation();
        
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<TestImplementation>(implementation));
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<ITestInterface, WrongImplementation>());
        // SetupShouldThrowDeveloperException(x => x.RegisterSingleton<TestImplementation, AnApiDefinition>(implementation));
        // SetupShouldThrowDeveloperException(x => x.RegisterSingleton<ITestInterface, WrongImplementation, AnApiDefinition>());
    }

    [TestMethod]
    public void NoRoomForTwo()
    {        
        var locator = new ServiceLocator();
        var implementation = new TestImplementation();

        locator.RegisterSingleton<ITestInterface>(implementation);

        ShouldThrowDeveloperException(() => locator.RegisterSingleton<ITestInterface>(implementation));        
        ShouldThrowDeveloperException(() => locator.RegisterSingleton<ITestInterface, TestImplementation>());
    }

    [TestMethod]
    public void StoillNoRoomForTwo()
    {        
        var locator = new ServiceLocator();
        var implementation = new TestImplementation();

        locator.RegisterSingleton<ITestInterface, TestImplementation>();

        ShouldThrowDeveloperException(() => locator.RegisterSingleton<ITestInterface>(implementation));        
        ShouldThrowDeveloperException(() => locator.RegisterSingleton<ITestInterface, TestImplementation>());
    }
    
    [TestMethod]
    public void DontAskForWhatWasNeverGiven()
    {        
        var locator = new ServiceLocator();

        ShouldThrowDeveloperException(() => locator.Get<IWrongInterface>());
    }

    [TestMethod]
    public void YouGetWhatYouGive()
    {        
        var locator = new ServiceLocator();
        var implementation = new TestImplementation();

        locator.RegisterSingleton<ITestInterface>(implementation);

        var test = locator.Get<ITestInterface>();

        Assert.AreSame(implementation, test);
    }

    [TestMethod]
    public void YouGetComplexThingsBack()
    {
        var locator = new ServiceLocator();
        
        locator.RegisterSingleton<IWrongInterface, WrongImplementation>();
        locator.RegisterSingleton<ITestInterface, ComplexImplementation>();

        var test = locator.Get<ITestInterface>();

        Assert.IsNotNull(test);        
        Assert.IsTrue(test is ComplexImplementation);
    }

    [TestMethod]
    public void YouGetWhatYouSuggest()
    {        
        var locator = new ServiceLocator();

        locator.RegisterSingleton<ITestInterface, TestImplementation>();

        var test = locator.Get<ITestInterface>();

        Assert.IsNotNull(test);        
        Assert.IsTrue(test is TestImplementation);
    }

    [TestMethod]
    public void SingletonsStaySingle()
    {
        var locator = new ServiceLocator();

        locator.RegisterSingleton<ITestInterface, TestImplementation>();

        var test = locator.Get<ITestInterface>();
        var test2 = locator.Get<ITestInterface>();

        Assert.AreSame(test, test2);
    }

    // [TestMethod]
    // public void YouGetWhatYouGiveForAnApi()
    // {        
    //     var locator = new ServiceLocator();
    //     var implementation = new TestImplementation();

    //     locator.RegisterSingleton<ITestInterface, AnApiDefinition>(implementation);

    //     var test = locator.Get<ITestInterface, AnApiDefinition>();

    //     Assert.IsNotNull(test);
    // }

    // [TestMethod]
    // public void YouGetWhatYouSuggestForAnApi()
    // {        
    //     var locator = new ServiceLocator();

    //     locator.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>();

    //     var test = locator.Get<ITestInterface, AnApiDefinition>();

    //     Assert.IsNotNull(test);
    // }

    // [TestMethod]
    // public void ThisIsNotForYourAPI()
    // {        
    //     var locator = new ServiceLocator();

    //     locator.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>();

    //     Assert.ThrowsException<DeveloperException>(() => 
    //         locator.Get<ITestInterface, AnotherApiDefinition>());
    // }

    // [TestMethod]
    // public void NoRoomForTwoInAnApi()
    // {        
    //     var locator = new ServiceLocator();

    //     locator.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>();

    //     Assert.ThrowsException<DeveloperException>(() => 
    //         locator.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>());
    // }

    private static void SetupShouldThrowDeveloperException(Action<IServiceLocatorSetup> setup)
    {    
        Assert.ThrowsException<DeveloperException>(() => NewSetup(setup));
    }

    private static void ShouldThrowDeveloperException(Action action)
    {    
        Assert.ThrowsException<DeveloperException>(() => action());
    }
    
    private static IServiceLocatorSetup NewSetup(Action<IServiceLocatorSetup> act)
    {
        var result = new ServiceLocator();

        act(result);

        return result;
    }
}