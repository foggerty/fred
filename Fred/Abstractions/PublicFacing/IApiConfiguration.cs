namespace Fred.Abstractions.PublicFacing
{
    public interface IApiConfiguration
    {
        /* Configure APIs and Endpoints */
                
        public IApiConfiguration RegisterEndpoint<A, E, Q>()
            where A : IApiDefinition, new()
            where E : IApiEndpointHandler<Q>, new();

        /* Configure dependency injection. */
        
        public IApiConfiguration SetupServices(Action<IServiceLocatorSetup> setup);

        /* Certificate setup */

        public IApiConfiguration UseSelfSignedCertificate();

        public IApiConfiguration UseCertificate(string store, string thumbprint);

        /* Access various local resources */

        public IApiConfiguration AllowAccessToFileSystem();

        /* Finish configuration, spin up server and hand over control. */
        
        public IServer Done();        
    }
}