using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Implimentations.Internal;

internal class ServiceLocator : IServiceLocator, IServiceLocatorSetup
{
    public T Get<T>()
    {
        throw new NotImplementedException();
    }

    public T Get<T, API>() where API : IApiDefinition
    {
        throw new NotImplementedException();
    }

    T IServiceLocator.Get<T>()
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterSession<T>(Func<T>? create)
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterSession<T, API>(Func<T> create)
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterSingleton<T>(Func<T>? create)
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterSingleton<T, API>(Func<T> create)
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterTransient<T>(Func<T>? create)
    {
        throw new NotImplementedException();
    }

    void IServiceLocatorSetup.RegisterTransient<T, API>(Func<T> create)
    {
        throw new NotImplementedException();
    }
}