using System.ComponentModel.Design;
using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Functions;

namespace Fred.Implimentations.Internal.Services;

internal enum ServiceLifetime
{
    Transient,
    Static
}

internal class ServiceLocator : IServiceLocator, IServiceLocatorSetup
{    
    private readonly ServiceContainer _singletons = new();
    private readonly ServiceContainer _apiSingletons = new();
        
    public I Get<I>()
    {
        typeof(I).MustBeInterface();                    
        
        var service = FromCache<I>() ?? Create<I>();

        if(service == null)
            throw new Exception($"Service '{typeof(I)}' not found in _singletons.");

        return (I)service;
    }

    public I Get<I, API>() where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I>(I instance)
    {
        typeof(I).MustBeInterface();

        if(instance == null)
            throw new Exception($"Cannot register a null instance for interface '{nameof(I)}'");

        _singletons.AddService(typeof(I), instance);
    }

    public void RegisterSingleton<I, API>(I instance)
        where API : IApiDefinition
    {
        throw new NotImplementedException();
    }
    
    public void RegisterSingleton<I, T>()
        where T : new()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));

        // test if already registered
    }

    public void RegisterSingleton<I, T, API>()
        where T : new()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(T));

        // test if already registered
    }

    public void RegisterTransient<I, T>()
        where T : new()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        
        // test if already registered
    }

    public void RegisterTransient<I, T, API>()
        where T : new()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        
        // test if already registered
    }

    private T Create<T>()
    {
        throw new NotImplementedException();
    }

    private I FromCache<I>()
    {
        throw new NotImplementedException();
    }
}