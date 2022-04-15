using System.ComponentModel.Design;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Internal;

internal enum ServiceLifetime
{
    Transient,
    Static
}

internal class Service
{
    internal Service(IApi? api, ServiceLifetime lifetime)
    {
        Api = api;
        Lifetime = lifetime;
    }   
    
    internal IApi? Api { get; }
    internal ServiceLifetime Lifetime { get; }
}

internal class ServiceLocator : IServiceLocator, IServiceLocatorSetup
{
    private readonly ServiceContainer _singletons = new();
    
    public I Get<I>()
    {
        typeof(I).MustBeInterface();
                
        var service = _singletons.GetService(typeof(I));

        if(service == null)
            throw new DeveloperException($"You asked for service '{typeof(I)}', but none was provided.");

        return (I)service;
    }

    public void RegisterSingleton<I, T>()
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I, T, API>()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterSingleton<I>(Func<I> create)
    {
        typeof(I).MustBeInterface();
        
        var instance = create();

        if(instance == null)
            throw new DeveloperException("Come one now.  It's your own time that you're wasting.");

        _singletons.AddService(typeof(I), instance);
    }

    public void RegisterSingleton<I, API>(Func<I> create)
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T>()
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T, API>()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I>(Func<I> create)
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, API>(Func<I> create)
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        
        throw new NotImplementedException();
    }
}