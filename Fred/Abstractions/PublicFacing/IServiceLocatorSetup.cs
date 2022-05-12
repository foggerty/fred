using Fred.Abstractions.Internal.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IServiceLocatorSetup : IServiceLocator
{
    public void RegisterSingleton<T>(T instance);

    public void RegisterSingleton<T, API>(T instance)
        where API : IApiDefinition;
    
    public void RegisterSingleton<I, T>()
        where T : new();

    public void RegisterSingleton<I, T, API>()
        where T : new()
        where API : IApiDefinition;
    
    public void RegisterTransient<I, T>()
        where T : new();

    public void RegisterTransient<I, T, API>()
        where T : new()
        where API : IApiDefinition;

}