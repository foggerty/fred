using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IApiConfig<A> : IFredService
    where A : IApiDefinition
{
    // Used to tag config for a given API.
}