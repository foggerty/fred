namespace Fred.Abstractions.PublicFacing.Services;

public interface IServices
{
    public I Get<I>()
        where I : IFredService;

    public I Get<I, A>()
        where I : IFredService
        where A : class, IApiDefinition;
}