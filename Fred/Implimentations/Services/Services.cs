using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Implimentations.Services;

namespace Fred.Abstractions.Internal.Services;

internal class ApiServices : IApiServices, IApiServicesSetup
{
    private readonly ServiceFactory _globalSingletons = new();
    private readonly Dictionary<Type, IServiceFactory> _apiSingletons = new();

    private const string YouDidNotRegister = "You failed to register {0} for the api {1}.  'F.  For Failure.' - Steven He.";
    
    public I Get<I>()
    {
        return _globalSingletons.Get<I>();
    }

    public I Get<I, A>() where A : IApiDefinition
    {
        if (!_apiSingletons.TryGetValue(typeof(A), out var serviceFactory))
            throw new DeveloperException(YouDidNotRegister, typeof(I), typeof(A));

        return serviceFactory.Get<I>();
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