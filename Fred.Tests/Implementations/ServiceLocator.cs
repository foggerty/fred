using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Services;

namespace Fred.Tests;

[TestClass]
public class ServiceLocatorTests
{
    private interface ITestInterface {}
    private class TestImplementation: ITestInterface {}
    private class WrongClass {}
    private class AnApiDefinition : IApiDefinition
    {
        public string Name => "Test";
        public string Description => "Testing";
        public Version Version => new("1");
        public string Root => "The Roots";
    }

    [TestMethod]
    public void CanOnlyRegisterInterfaces()
    {
        var implementation = new TestImplementation();
        
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<TestImplementation>(implementation));
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<ITestInterface, WrongClass>());
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<TestImplementation, AnApiDefinition>(implementation));
        SetupShouldThrowDeveloperException(x => x.RegisterSingleton<ITestInterface, WrongClass, AnApiDefinition>());
    }

    private static void SetupShouldThrowDeveloperException(Action<IServiceLocatorSetup> setup)
    {    
        Assert.ThrowsException<DeveloperException>(() => NewSetup(setup));
    }
    
    private static IServiceLocatorSetup NewSetup(Action<IServiceLocatorSetup> act)
    {
        var result = new ServiceLocator();

        act(result);

        return result;
    }
}