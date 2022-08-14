using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal interface IServerConfiguration : IServerController
{
    public void AddHandler<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>;

    public void UseCertificate(X509Certificate certificate);
}