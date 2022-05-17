using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal interface IServerConfiguration : IServer
{
    internal IServerConfiguration AddHandler<A, E, Q>()
            where A : IApiDefinition
            where E : IApiEndpointHandler<Q>;

    internal IServerConfiguration UseHttpsCertificate(X509Certificate certificate);
}