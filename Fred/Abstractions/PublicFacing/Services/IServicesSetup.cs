namespace Fred.Abstractions.PublicFacing.Services;

public interface IServicesSetup : IServices
{
    // Register globally available services.

    public void RegisterSingleton<I>(I instance)
        where I : IFredService;

    public void RegisterSingleton<I, T>()
        where I : IFredService
        where T : IFredService;

    public void RegisterSingleton<I>(Func<I> creator)
        where I : IFredService;

    // Register services for specific APIs.

    public void RegisterSingleton<I, A>(I instance)
        where I : IFredService
        where A : IApiDefinition;

    public void RegisterSingleton<I, T, A>()
        where I : IFredService
        where T : IFredService
        where A : IApiDefinition;

    public void RegisterSingleton<I, A>(Func<I> creator)
        where I : IFredService
        where A : IApiDefinition;
}