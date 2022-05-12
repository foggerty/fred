using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal.Services;

public interface IServiceLocator
{
    public I Get<I>();

    public I Get<I, API>()
        where API : IApiDefinition;
}

