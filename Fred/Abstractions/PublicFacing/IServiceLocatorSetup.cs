using Fred.Abstractions.Internal.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IServiceLocatorSetup : IServiceLocator
{
    // Singletons available to all APIs.
    
    public void RegisterSingleton<I>(I instance);

    public void RegisterSingleton<I, T>()
        where T : new();
    
    // Singletons available to given APIs.
    
    public void RegisterSingleton<I, API>(I instance)
        where API : IApiDefinition;
       
    public void RegisterSingleton<I, T, API>()
        where T : new()
        where API : IApiDefinition;
}