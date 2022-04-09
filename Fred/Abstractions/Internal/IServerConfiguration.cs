using System.Security.Cryptography.X509Certificates;
using Fred.Abstractions.PublicFacing;

namespace Fred.Abstractions.Internal;

internal interface IServerConfiguration : IServer
{
    internal IServerConfiguration AddHandler<A, E, Q>()
            where A : IApiDefinition, new()
            where E : IApiEndpointHandler<Q>, new();

    internal IServerConfiguration UseHttpsCertificate(X509Certificate certificate);
}