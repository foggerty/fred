using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal interface IServerConfiguration : IServerController
{
    internal void AddHandler<A, E, Q>()
            where A : IApiDefinition
            where E : IApiEndpointHandler<Q>;

    internal void UseCertificate(X509Certificate certificate);
}