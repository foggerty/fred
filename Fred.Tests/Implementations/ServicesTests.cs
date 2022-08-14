using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Implementations.Services;

// ReSharper disable All

namespace Fred.Tests.Implementations;

[TestClass]
public class ServicesTests
{
    [TestMethod]
    public void RegistrationMistakes()
    {
        // Can only register interfaces globally.
        BadRegistrationThrowsDeveloperException(x => x.RegisterSingleton<TestImplementation>(new TestImplementation()));

        // Can only register interface/implementation pair globally.
        BadRegistrationThrowsDeveloperException(x => x.RegisterSingleton<ITestInterface, WrongImplementation>());

        // Can only register interfaces for an API.        
        BadRegistrationThrowsDeveloperException(x =>
            x.RegisterSingleton<TestImplementation, AnApiDefinition>(new TestImplementation()));

        // Can only register interface/implementation pair for an API.
        BadRegistrationThrowsDeveloperException(x =>
            x.RegisterSingleton<ITestInterface, WrongImplementation, AnApiDefinition>());

        // Can only register an implementation that has a valid constructor.
        BadRegistrationThrowsDeveloperException(x => x.RegisterSingleton<IBadConstructor, BadConstructor>());

        // Can only register an implementations that has a valid constructor for an API.
        BadRegistrationThrowsDeveloperException(x =>
            x.RegisterSingleton<IBadConstructor, BadConstructor, AnApiDefinition>());

        // Can only register implementations if they have constructors we can use.
        BadRegistrationThrowsDeveloperException(x => x.RegisterSingleton<IBadConstructor, BadConstructor>());
    }

    [TestMethod]
    public void NoRoomForTwo()
    {
        var implementation = new TestImplementation();

        // Can only register an interface globally once.        
        BadRegistrationThrowsDeveloperException(
            setup => setup.RegisterSingleton<ITestInterface>(implementation),
            test => test.RegisterSingleton<ITestInterface>(implementation),
            test => test.RegisterSingleton<ITestInterface, TestImplementation>()
        );
    }

    [TestMethod]
    public void StillNoRoomForTwo()
    {
        var implementation = new TestImplementation();

        // Can only register an interface/implementation globally once.
        BadRegistrationThrowsDeveloperException(
            setup => setup.RegisterSingleton<ITestInterface, TestImplementation>(),
            test => test.RegisterSingleton<ITestInterface>(implementation),
            test => test.RegisterSingleton<ITestInterface, TestImplementation>()
        );
    }

    [TestMethod]
    public void DontAskForWhatWasNeverGiven()
    {
        var provider = new Services();

        // Can not 'Get' something that isn't there.
        ShouldThrowDeveloperException(() => provider.Get<IWrongInterface>());
    }

    [TestMethod]
    public void YouGetWhatYouGive()
    {
        var provider       = new Services();
        var implementation = new TestImplementation();

        provider.RegisterSingleton<ITestInterface>(implementation);

        var test = provider.Get<ITestInterface>();

        Assert.AreSame(implementation, test);
    }

    [TestMethod]
    public void YouGetComplexThingsBack()
    {
        var provider = new Services();

        provider.RegisterSingleton<ISomeOtherInterface, SomeOtherImplementation>();
        provider.RegisterSingleton<ITestInterface, ComplexImplementation>();

        var test = provider.Get<ITestInterface>();

        Assert.IsNotNull(test);
        Assert.IsTrue(test is ComplexImplementation);
    }

    [TestMethod]
    public void YouGetWhatYouSuggest()
    {
        var provider = new Services();

        provider.RegisterSingleton<ITestInterface, TestImplementation>();

        var test = provider.Get<ITestInterface>();

        Assert.IsNotNull(test);
        Assert.IsTrue(test is TestImplementation);
    }

    [TestMethod]
    public void SingletonsStaySingle()
    {
        var provider = new Services();

        provider.RegisterSingleton<ITestInterface, TestImplementation>();

        var test  = provider.Get<ITestInterface>();
        var test2 = provider.Get<ITestInterface>();

        Assert.AreSame(test, test2);
    }

    [TestMethod]
    public void YouGetWhatYouGiveForAnApi()
    {
        var provider       = new Services();
        var implementation = new TestImplementation();

        provider.RegisterSingleton<ITestInterface, AnApiDefinition>(implementation);

        var test = provider.Get<ITestInterface, AnApiDefinition>();

        Assert.IsNotNull(test);
    }

    [TestMethod]
    public void YouGetWhatYouSuggestForAnApi()
    {
        var provider = new Services();

        provider.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>();

        var test = provider.Get<ITestInterface, AnApiDefinition>();

        Assert.IsNotNull(test);
    }

    [TestMethod]
    public void ThisIsNotForYourAPI()
    {
        var provider = new Services();

        provider.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>();

        ShouldThrowDeveloperException(() => provider.Get<ITestInterface, AnotherApiDefinition>());
    }

    [TestMethod]
    public void NoRoomForTwoInAnApi()
    {
        BadRegistrationThrowsDeveloperException(
            setup => setup.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>(),
            test => test.RegisterSingleton<ITestInterface, TestImplementation, AnApiDefinition>()
        );
    }

    private static void BadRegistrationThrowsDeveloperException(Action<IServicesSetup> setup)
    {
        var provider = new Services();

        Assert.ThrowsException<DeveloperException>(() => setup(provider));
    }

    private static void ShouldThrowDeveloperException(Action action)
    {
        Assert.ThrowsException<DeveloperException>(action);
    }

    private static void BadRegistrationThrowsDeveloperException(
        Action<IServicesSetup>          setup,
        params Action<IServicesSetup>[] tests)
    {
        var services = new Services();

        setup(services);

        foreach (var test in tests)
            Assert.ThrowsException<DeveloperException>(() => test(services));
    }

    #region test data

    private interface ITestInterface : IFredService
    {
    }

    private interface IWrongInterface : IFredService
    {
    }

    private interface ISomeOtherInterface : IFredService
    {
    }

    private class SomeOtherImplementation : ISomeOtherInterface
    {
    }

    private class TestImplementation : ITestInterface
    {
    }

    private class ComplexImplementation : ITestInterface
    {
        public ComplexImplementation(ISomeOtherInterface someOtherImplementation)
        {
            Assert.IsNotNull(someOtherImplementation);
        }
    }

    private class WrongImplementation : IWrongInterface
    {
    }

    private record AnApiDefinition : IApiDefinition
    {
        public string Name => "Test";
        public string Description => "Testing";
        public Version Version => new("1");
        public string Root => "The Roots";
    }

    private record AnotherApiDefinition : IApiDefinition
    {
        public string Name => "Another Test";
        public string Description => "Testing";
        public Version Version => new("1");
        public string Root => "The Roots";
    }

    private interface IBadConstructor : IFredService
    {
    }

    public class BadConstructor : IBadConstructor
    {
        public BadConstructor(int a)
        {
        }
    }

    #endregion
}