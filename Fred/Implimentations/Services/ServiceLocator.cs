using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Services;

internal class ServiceLocator : IServiceLocatorSetup, IServiceLocator
{    
    private readonly Dictionary<Type, Type> _toCreate = new();
    private readonly ServiceContainer _singletons = new();

    private const string PleaseRegister = "Before you ask for '{0}', first register it.  Maybe you registered it for a specified API?";
    private const string CannotRegisterANull = "Cannot register a null instance for interface '{0}' (or ANY interface, for that matter).  I mean, seriously...";
    private const string DoNotRegisterTwice = "You've tried to register {0} twice.  Please refrain from doing so again.";
        
    public I Get<I>()
    {
        typeof(I).MustBeInterface();
        
        var service = FromSingletons<I>() ?? NewInstance<I>();

        if(service == null)
        {
            throw new DeveloperException(PleaseRegister, typeof(I));
        }                              

        return (I)service;
    }

    public void RegisterSingleton<I>(I instance)
    {
        typeof(I).MustBeInterface();

        if(instance == null)
            throw new DeveloperException(CannotRegisterANull, nameof(I));

        TestNotAlreadyRegistered<I>();

        _singletons.AddService(typeof(I), instance);
    }

    public void RegisterSingleton<I, T>()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        typeof(T).MustHavePublicConstructor();

        TestNotAlreadyRegistered<I>();
        
        _toCreate[typeof(I)] = typeof(T);
    }

    private void TestNotAlreadyRegistered<I>()
    {
        if(_singletons.GetService(typeof(I)) != null)
            throw new DeveloperException(DoNotRegisterTwice, typeof(I));
        
        if(_toCreate.ContainsKey(typeof(I)))
            throw new DeveloperException(DoNotRegisterTwice, nameof(I));
    }
    
    private I? FromSingletons<I>()
    {
        var result = _singletons.GetService(typeof(I));

        return result == null
            ? default
            : (I)result;
    }

    private I? NewInstance<I>()
    {
        if(!_toCreate.ContainsKey(typeof(I)))
            return default;
        
        var typeToCreate = _toCreate[typeof(I)];

        var constructor = typeToCreate.GetConstructor(Array.Empty<Type>());

        if(constructor == null)
            throw new DeveloperException("It's all gone horribly wrong, this exception shouldn't even be seen :-(");

        var instance = (I)constructor.Invoke(null);

        _singletons.AddService(typeof(I), instance);

        return instance;
    }
}