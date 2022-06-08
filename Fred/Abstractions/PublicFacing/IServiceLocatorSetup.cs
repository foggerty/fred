using Fred.Abstractions.Internal.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IServiceLocatorSetup : IServiceLocator
{
    // Singletons available to all APIs.
    
    public void RegisterSingleton<I>(I instance);

    public void RegisterSingleton<I, T>();
}