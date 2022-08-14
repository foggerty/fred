using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IConfig : IFredService
{
    IApiConfig<A> ConfigFor<A>()
        where A : IApiDefinition;
}