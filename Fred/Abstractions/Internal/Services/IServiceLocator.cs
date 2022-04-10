using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal.Services;

internal partial interface IApiServiceLocator
{
    public T Get<T, API>()
        where API : IApiDefinition;
}