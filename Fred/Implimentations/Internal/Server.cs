using System.Security.Cryptography.X509Certificates;
using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Functions;

namespace Fred.Implimentations.Internal;

internal class Server : IServerConfiguration
{
    private WebApplication _app;
    private X509Certificate? _certificate;

    private bool _mapped = false;
        
    private readonly IConfig _settings;
    private readonly IServiceLocator _locator;
    
    public Server(IConfig settings, IServiceLocator locator)
    {
        _settings = settings;
        _locator = locator;

        _app = Bootstrap.NewWebApp(settings);
    }
    
    public IServerConfiguration AddHandler<A, E, Q>()
        where A : IApiDefinition, new()
        where E : IApiEndpointHandler<Q>, new()
    {
        var apiDefinition = new A();
        var endpoint = new E();        // get from DI

        _app.MapGet(apiDefinition.PathTo(endpoint), NewHandlerDelegate<Q>(apiDefinition, endpoint));
        
        return this;
    }

    public IServerConfiguration UseHttpsCertificate(X509Certificate certificate)
    {
        _certificate = certificate;

        return this;
    }
    
    public void StartApis(TimeSpan timeout)
    {        
        _app.Start();
    }

    public void StopApis(TimeSpan timeout)
    {
        _app.StopAsync(timeout);
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