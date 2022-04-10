using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

public interface IServiceLocatorSetup
{
    internal void RegisterSingleton<T>(Func<T> create);

    internal void RegisterSingleton<T, API>(Func<T> create)
        where API : IApiDefinition;

    internal void RegisterTransient<T>(Func<T> create);

    internal void RegisterTransient<T, API>(Func<T> create)
        where API : IApiDefinition;
    
    internal void RegisterSession<T>(Func<T> create);

    internal void RegisterSession<T, API>(Func<T> create)
        where API : IApiDefinition;
}