using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Implimentations.Services;

namespace Fred.Abstractions.Internal.Services;

internal class ApiServices : IApiServices, IApiServicesSetup
{
    private readonly ServiceFactory _globalSingletons = new();
    private readonly Dictionary<Type, IServiceFactory> _apiSingletons = new();

    private const string YouDidNotRegister = "Why did you not register the type {0}?  WHY?!?";
        
    public I Get<I>()
    {
        var instance = _globalSingletons.Get<I>();

        if(instance == null)
            throw new DeveloperException(YouDidNotRegister, typeof(I).Name);

        return instance;
    }
    
    public I Get<I, A>()
        where A : IApiDefinition
    {
        // Get API specific service if registered, otherwise will get global service.
        
        if (_apiSingletons.TryGetValue(typeof(A), out var serviceFactory))
        {
            var instance = serviceFactory.Get<I>();

            if(instance != null)
                return instance;
        }

        return Get<I>();
    }

    public void RegisterSingleton<I>(I instance)
    {
        _globalSingletons.RegisterSingleton<I>(instance);
    }

    public void RegisterSingleton<I, T>()
    {
        _globalSingletons.RegisterSingleton<I, T>();
    }

    public void RegisterSingleton<I, A>(I instance)
        where A : IApiDefinition
    {                
        var serviceFactory = FactoryFor<A>();

        serviceFactory.RegisterSingleton<I>(instance);
    }

    public void RegisterSingleton<I, T, A>()
        where A : IApiDefinition
    {
        var serviceFactory = FactoryFor<A>();

        serviceFactory.RegisterSingleton<I, T>();
    }

    private IServiceFactory FactoryFor<A>()
        where A : IApiDefinition
    {
        if (!_apiSingletons.TryGetValue(typeof(A), out var serviceFactory))
        {
            serviceFactory = new ServiceFactory();
            
            _apiSingletons.Add(typeof(A), serviceFactory);
        }

        return serviceFactory;
    }
}