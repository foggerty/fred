using Fred.Abstractions.PublicFacing.Services;

namespace Fred.Abstractions.PublicFacing;

public interface IApiSetup
{
    /* Configure APIs and Endpoints */

    public IApiSetup RegisterConfig<A, C>()
        where A : IApiDefinition
        where C : IApiConfig<A>;
    
    public IApiSetup AddHandler<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>;

    /* Configure dependency injection. */

    public IApiSetup ConfigureServices(Action<IServicesSetup> setup);

    /* Certificate setup. */

    public IApiSetup UseSelfSignedCertificate();

    public IApiSetup UseCertificate(string store, string thumbprint);

    /* Access various local resources. */

    public IApiSetup AllowAccessToFileSystem();

    /* Finish configuration, spin up server and hand over control. */

    public IServerController Done();
}