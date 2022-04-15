using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IServiceLocatorSetup : IServiceLocator
{
    public void RegisterSingleton<I, T>();

    public void RegisterSingleton<I, T, API>()
        where API : IApiDefinition;

    public void RegisterSingleton<I>(Func<IServiceLocator, I> create);

    public void RegisterSingleton<I, API>(Func<IServiceLocator, I> create)
        where API : IApiDefinition;

    public void RegisterSingleton<I, T>(Func<IServiceLocator, T> create);

    public void RegisterSingleton<I, T, API>(Func<IServiceLocator, T> create)
        where API : IApiDefinition;

    public void RegisterTransient<I, T>();

    public void RegisterTransient<I, T, API>()
        where API : IApiDefinition;

    public void RegisterTransient<I, T>(Func<IServiceLocator, T> create);

    public void RegisterTransient<I, T, API>(Func<IServiceLocator, T> create)
        where API : IApiDefinition;
}