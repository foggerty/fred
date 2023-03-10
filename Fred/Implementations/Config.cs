using Fred.Abstractions.PublicFacing;

namespace Fred.Implementations;

public class Config : IConfig
{
    public IApiConfig<A> ConfigFor<A>() where A : IApiDefinition
    {
        throw new NotImplementedException();
    }
}