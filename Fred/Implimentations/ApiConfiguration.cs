using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;
using Fred.Implimentations.Services; 

namespace Fred.Implimentations;

internal class ApiConfiguration : IApiConfiguration
{
    private bool _allowAccessToFileSystem;
    
    private readonly IServerConfiguration _server;
    private readonly IApiServicesSetup _serviceSetup;
    private readonly IConfig _config;

    internal ApiConfiguration(
        IServerConfiguration server,
        IApiServicesSetup locator,
        IConfig config)
    {
        _server = server;
        _serviceSetup = locator;
        _config = config;
    }

    #region Configure APIs and Endpoints

    public IApiConfiguration RegisterEndpoint<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>
    {
        _server.AddHandler<A, E, Q>();

        return this;
    }

    #endregion

    #region Configure dependency injection

    public IApiConfiguration AddServices(Action<IApiServicesSetup, IConfig> setup)
    {
        setup(_serviceSetup, _config);

        return this;
    }

    #endregion

    #region Certificate setup

    public IApiConfiguration UseSelfSignedCertificate()
    {
        //var cert =  new X509Certificate2();        
        //var store = new X509Store();
        //string storeName = "";
        //string thumbprint = "";

        //store.Open(OpenFlags.ReadWrite);
        //store.Add(cert);

        // write to store so can be exported - ToDo: add functionality to save to path and skip store.

        // get thumbprint

        //store.Close();

        //return UseCertificate(storeName, thumbprint);

        return this;
    }

    public IApiConfiguration UseCertificate(string storeName, string thumbprint)
    {
        // ToDo - check if store actually exists

        var store = new X509Store(storeName);

        store.Open(OpenFlags.ReadOnly);

        var cert = store
            .Certificates
            .FirstOrDefault(c => c.Thumbprint.EqualsThumbprint(thumbprint));

        if (cert == null)
            throw new DeveloperException($"You asked me to supply you with a certificate having this thumbprint: {thumbprint}\n" +
                                         $"that you assured me was in the store: {storeName}." +
                                          "Sadly, I could not find it.  Perhaps you mislaid it?");

        store.Close();

        _server.UseCertificate(cert);

        return this;
    }

    #endregion

    #region Open up access to local resources

    public IApiConfiguration AllowAccessToFileSystem()
    {
        _allowAccessToFileSystem = true;

        return this;
    }

    #endregion

    #region Finish configuration, spin up server and hand over control

    public IServerController Done()
    {
        if (_allowAccessToFileSystem)
            _serviceSetup.RegisterSingleton<ITemporaryFileSystem>(new TemporaryFileSystem());

        return _server;
    }

    #endregion
}