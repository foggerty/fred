using System.Security.Cryptography.X509Certificates;
using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Implimentations.Internal;

internal class Server : IServerConfiguration, IServer
{
    private X509Certificate? _certificate;

    private readonly ApiMap _map = new();
        
    private readonly IConfiguration _configuration;
    private readonly IServiceLocator _locator;
    
    public Server(IConfiguration configuration, IServiceLocator locator)
    {
        _configuration = configuration;
        _locator = locator;
    }
    
    public IServerConfiguration AddHandler<A, E, Q>()
        where A : IApiDefinition, new()
        where E : IApiEndpointHandler<Q>, new()
    {
        _map.AddEndpoint<A, E, Q>();  // map should create one of each maybe?  Avoid creation errors at runtime?

        return this;
    }

    public IServerConfiguration UseHttpsCertificate(X509Certificate certificate)
    {
        _certificate = certificate;

        return this;
    }
    
    public void StartApis(TimeSpan timeout)
    {
        throw new NotImplementedException();
    }

    public void StopApis(TimeSpan timeout)
    {
        throw new NotImplementedException();
    }        
}