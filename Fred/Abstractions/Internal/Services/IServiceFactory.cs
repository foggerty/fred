namespace Fred.Abstractions.Internal.Services;

internal interface IServiceFactory
{
    public I Get<I>();

    public void RegisterSingleton<I>(I instance);

    public void RegisterSingleton<I, T>();
}

