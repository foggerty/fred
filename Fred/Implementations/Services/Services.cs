using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Functions;

namespace Fred.Implementations.Services;

internal class Services : IServicesSetup
{
    private readonly Dictionary<Type, IServiceFactory> _apiSingletons = new();
    private readonly ServiceFactory _globalSingletons = new();

    public I Get<I>()
        where I : IFredService
    {
        return _globalSingletons.Get<I>();
    }

    public I Get<I, A>()
        where I : IFredService
        where A : class, IApiDefinition
    {
        typeof(A).MustBeClass(); // ToDo - again, why is the compiler not enforcing this?
        
        return _apiSingletons.ContainsKey(typeof(A))
            ? _apiSingletons[typeof(A)].Get<I>()
            : Get<I>();
    }

    public void RegisterSingleton<I>(I instance)
        where I : IFredService
    {
        _globalSingletons.RegisterSingleton<I>(instance);
    }

    public void RegisterSingleton<I, T>()
        where I : IFredService
        where T : class, IFredService
    {
        _globalSingletons.RegisterSingleton<I, T>();
    }

    public void RegisterSingleton<I>(Func<I> creator)
        where I : IFredService
    {
        _globalSingletons.RegisterSingleton(creator);
    }

    public void RegisterSingleton<I, A>(I instance)
        where I : IFredService
        where A : class
    {
        typeof(A).MustBeClass(); // I have NO idea why the compiler isn't 
        
        var serviceFactory = FactoryFor<A>();

        serviceFactory.RegisterSingleton<I>(instance);
    }

    public void RegisterSingleton<I, T, A>()
        where I : IFredService
        where T : class, IFredService
        where A : class, IApiDefinition
    {
        typeof(A).MustBeClass();
        
        var serviceFactory = FactoryFor<A>();

        serviceFactory.RegisterSingleton<I, T>();
    }

    public void RegisterSingleton<I, A>(Func<I> creator)
        where I : IFredService
        where A : class, IApiDefinition
    {
        typeof(A).MustBeClass();
        
        var serviceFactory = FactoryFor<A>();

        serviceFactory.RegisterSingleton<I>(creator);
    }

    private IServiceFactory FactoryFor<A>()
        where A : class
    {
        typeof(A).MustBeClass();
        
        if (_apiSingletons.TryGetValue(typeof(A), out var serviceFactory))
            return serviceFactory;

        serviceFactory = new ServiceFactory();

        _apiSingletons.Add(typeof(A), serviceFactory);

        return serviceFactory;
    }
}