using System.Data;
using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IServiceLocatorSetup : IServiceLocator
{
    public void RegisterSingleton<I, T>();

    public void RegisterSingleton<I, T, API>()
        where API : IApiDefinition;
    
    public void RegisterSingleton<I>(Func<I> create);
    
    public void RegisterSingleton<I, API>(Func<I> create)
        where API : IApiDefinition;



    public void RegisterTransient<I, T>();

    public void RegisterTransient<I, T, API>()
        where API : IApiDefinition;

    public void RegisterTransient<I>(Func<I> create);
    
    public void RegisterTransient<I, API>(Func<I> create)
        where API : IApiDefinition;
}