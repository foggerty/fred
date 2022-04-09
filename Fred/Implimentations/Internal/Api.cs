using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations.Internal;

internal class Api : IApi
{
    public Api(IApiDefinition definition)
    {
        Definition = definition;
        Endpoints = new List<IApiEndpoint>();
    }
    
    public IApiDefinition Definition { get; }

    public IEnumerable<IApiEndpoint> Endpoints { get; }
}
