using Fred.Abstractions.Internal;
using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;

namespace Fred.Implimentations;

internal class Server : IServerConfiguration
{
    private X509Certificate? _certificate;

    private readonly IApiServices _services;

    public Server(IApiServices services)
    {
        _services = services;

        // setup Kestrel       
    }

    public IServerConfiguration AddHandler<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>
    {
        // var apiDefinition = new A();
        // var endpoint = new E();        // get from DI

        return this;
    }

    public IServerConfiguration UseHttpsCertificate(X509Certificate certificate)
    {
        _certificate = certificate;

        return this;
    }

    public void StartApis(TimeSpan timeout)
    {

    }

    public void StopApis(TimeSpan timeout)
    {

    }

    private RequestDelegate NewHandlerDelegate<Q>(IApiDefinition apiDefinition, IApiEndpointHandler<Q> handler)
    {
        // An ApiWrapper CANOT expose RequestDelegate, because leaky abstraction.
        // An ApiWrapper should have a single function that takes an IApiEndpointHandler
        // and returns a Func<T, IAnswer>.  That is what is then called here.

        // Look for an API wrapper.

        return async x =>
        {
            // Standard headers check
            // Security check
            // Based on path, cast to Question type
            // Get handler from DI - each time in case transient dependencies.
            // questionHandler = wrapper ?? wrapper.Wrap(handler) : handler.Handler;
            // Get response from handler
            // Set response in context                       

            await Task.CompletedTask;
        };
    }
}