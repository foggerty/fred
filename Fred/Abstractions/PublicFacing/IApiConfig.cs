using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IApiConfig<T> : IFredService
    where T : IApiDefinition
{
    // Used to tag config for a given API.
}