using System.Security.Cryptography.X509Certificates;

using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations.Internal;

internal class ApiConfiguration : IApiConfiguration
{
    private readonly IServerConfiguration _server;
    private readonly IServiceLocatorSetup _serviceSetup;

    internal ApiConfiguration(IServerConfiguration server, IServiceLocatorSetup locator)
    {
        _server = server;
        _serviceSetup = locator;
    }
    
    #region Configure APIs and Endpoints

    public IApiConfiguration RegisterEndpoint<A, E, Q>()
            where A : IApiDefinition, new()
            where E : IApiEndpointHandler<Q>, new()
    {
        _server.AddHandler<A, E, Q>();
        
        return this;
    }

    #endregion

    #region Configure dependency injection

    public IApiConfiguration RegisterSingleton<T>(Func<T> create)
    {
        _serviceSetup.RegisterSession<T>(create);

        return this;
    }

    public IApiConfiguration RegisterSingleton<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        _serviceSetup.RegisterSingleton<T, API>(create);

        return this;
    }

    public IApiConfiguration RegisterTransient<T>(Func<T> create)
    {
        _serviceSetup.RegisterTransient<T>(create);

        return this;
    }

    public IApiConfiguration RegisterTransient<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        _serviceSetup.RegisterTransient<T, API>(create);

        return this;
    }

    public IApiConfiguration RegisterSession<T>(Func<T> create)
    {
        _serviceSetup.RegisterSession<T>(create);

        return this;
    }

    public IApiConfiguration RegisterSession<T, API>(Func<T> create)
        where API : IApiDefinition
    {
        _serviceSetup.RegisterSession<T, API>(create);

        return this;
    }    

    #endregion

    #region Certificate setup

    public IApiConfiguration UseSelfSignedCertificate()
    {
        var cert =  new X509Certificate2();        
        var store = new X509Store();

        store.Open(OpenFlags.ReadWrite);
        store.Add(cert);

        // write to store so can be exported - ToDo: add functionality to save to path and skip store.

        store.Close();

        _server.UseHttpsCertificate(cert);

         return this;
    }

    public IApiConfiguration UseCertificate(string storeName, string thumbprint)
    {
        var store = new X509Store(storeName);
        
        store.Open(OpenFlags.ReadOnly);

        var cert = store
            .Certificates
            .FirstOrDefault(c => c.Thumbprint.EqualsThumbprint(thumbprint));

        if(cert == null)
            throw new DeveloperException($"You asked me to supply you with a certificate with this thumbprint: {thumbprint}\n" +
                                          "and from that store: {storeName}." +
                                          "I could not find it.  Perhaps you misplaced it?");

        store.Close();

        _server.UseHttpsCertificate(cert);
        
        return this;
    }    

    #endregion
    
    #region Finish configuration, spin up server and hand over control

    public IServer Done()
    {        
        return _server;
    }

    #endregion    
}