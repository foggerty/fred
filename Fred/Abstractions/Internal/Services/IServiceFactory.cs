using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.Internal.Services;

internal interface IServiceFactory
{
    public I Get<I>()
        where I : IFredService;

    public void RegisterSingleton<I>(I instance)
        where I : IFredService;

    public void RegisterSingleton<I, T>()
        where I : IFredService
        where T : class;

    public void RegisterSingleton<I>(Func<I> creator)
        where I : IFredService;
}