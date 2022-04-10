using System.ComponentModel.Design;
using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Abstractions.Internal.Services;
using Fred.Exceptions;

namespace Fred.Implimentations.Internal;

internal class ServiceLocator : IServiceLocator, IApiServiceLocator, IServiceLocatorSetup
{
    private readonly ServiceContainer _services = new();
    
    public T Get<T>()
    {
        var result = _services.GetService(typeof(T));

        if(result == null)        
            throw new DeveloperException("");

        return (T)result;
    }

    public T Get<T, API>() where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void RegisterSession<T>(Func<T> create)
    {
        throw new NotImplementedException();
    }

    public void RegisterSession<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void RegisterSingleton<T>(Func<T> create)
    {
        throw new NotImplementedException();
    }

    public void RegisterSingleton<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    public void RegisterTransient<T>(Func<T> create)
    {
        throw new NotImplementedException();
    }

    public void RegisterTransient<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        throw new NotImplementedException();
    }
}