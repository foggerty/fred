namespace Fred.Abstractions.PublicFacing.Services;

public interface IApiServicesSetup
{
    // Register globally available services.
    
    public void RegisterSingleton<I>(I instance);

    public void RegisterSingleton<I, T>();

    // Register services for sepcific APIs.
    
    public void RegisterSingleton<I, A>(I instance)
        where A : IApiDefinition;

    public void RegisterSingleton<I, T, A>()
        where A : IApiDefinition;
}