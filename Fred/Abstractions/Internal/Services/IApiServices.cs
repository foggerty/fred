using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal.Services;

internal interface IApiServices
{
    public I Get<I>();

    public I Get<I, A>()
        where A : IApiDefinition;
}