using System.ComponentModel.Design;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
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
                
        var service = _singletons.GetService(typeof(I));

        if(service == null)
            throw new DeveloperException($"You asked for service '{typeof(I)}', but none was provided.");

        return (I)service;
    }

    public void RegisterSingleton<I, T>()
        where T : new()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));

        // test if already registered

        var instance = new T();       
        
        _singletons.AddService(typeof(I), instance);    
    }

    public void RegisterSingleton<I, T, API>()
        where T : new()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(T));

        // create
        var instance = new T();

        // get service container
        var container = _apiSingletons.GetService(typeof(API)) as ServiceContainer;

        if(container == null)
        {
            container = new ServiceContainer();

            _apiSingletons.AddService(typeof(API), container);
        }

        container.AddService(typeof(I), instance);    
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
        where T : new()
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        
        throw new NotImplementedException();
    }

    public void RegisterTransient<I, T, API>()
        where T : new()
        where API : IApiDefinition
    {
        typeof(I).MustBeInterface();
        typeof(T).MustImpliment(typeof(I));
        
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