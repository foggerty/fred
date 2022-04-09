namespace Fred.Abstractions.PublicFacing
{
    public interface IApiConfiguration
    {
        /* Configure APIs and Endpoints */
                
        public IApiConfiguration RegisterEndpoint<A, E, Q>()
            where A : IApiDefinition, new()
            where E : IApiEndpointHandler<Q>, new();

        /* Configure dependency injection. */
        
        public IApiConfiguration RegisterSingleton<T>(Func<T> create);

        public IApiConfiguration RegisterSingleton<T, API>(Func<T> create)
            where API : IApiDefinition;

        public IApiConfiguration RegisterTransient<T>(Func<T> create);

        public IApiConfiguration RegisterTransient<T, API>(Func<T> create)
            where API : IApiDefinition;

        public IApiConfiguration RegisterSession<T>(Func<T> create);

        public IApiConfiguration RegisterSession<T, API>(Func<T> create)
            where API : IApiDefinition;

        /* Certificate setup */

        public IApiConfiguration UseSelfSignedCertificate();

        public IApiConfiguration UseCertificate(string store, string thumbprint);

        /* Finish configuration, spin up server and hand over control. */
        
        public IServer Done();
    }
}