using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal interface IApiMap
{
    void AddEndpoint<API, ENDPOINT, Q>()
        where API : IApiDefinition, new()
        where ENDPOINT : IApiEndpointHandler<Q>, new();
}