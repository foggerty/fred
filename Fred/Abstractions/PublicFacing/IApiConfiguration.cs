using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IApiConfiguration
{
    /* Configure APIs and Endpoints */
            
    public IApiConfiguration RegisterEndpoint<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>;

    /* Configure dependency injection. */
    
    public IApiConfiguration AddServices(Action<IApiServicesSetup, IConfig> setup);

    /* Certificate setup. */

    public IApiConfiguration UseSelfSignedCertificate();

    public IApiConfiguration UseCertificate(string store, string thumbprint);

    /* Access various local resources. */

    public IApiConfiguration AllowAccessToFileSystem();

    /* Finish configuration, spin up server and hand over control. */
    
    public IServerController Done();        
}