using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;
using Fred.Implementations.Services;

namespace Fred.Implementations;

internal class ApiSetup : IApiSetup
{
    private readonly IServerConfiguration _server;
    private readonly IServicesSetup _servicesSetup;
    private bool _allowAccessToFileSystem;

    internal ApiSetup(
        IServerConfiguration server,
        IServicesSetup       servicesSetup)
    {
        _server        = server;
        _servicesSetup = servicesSetup;
    }

    #region Configure APIs and Endpoints

    public IApiSetup RegisterConfig<A, C>()
        where A : class, IApiDefinition 
        where C : class, IApiConfig<A>
    {
        return this;
    }

    public IApiSetup AddHandler<A, E, Q>()
        where A : class, IApiDefinition
        where E : class, IApiEndpointHandler<Q>
    {
        _server.AddHandler<A, E, Q>();

        return this;
    }

    #endregion

    #region Configure dependency injection

    public IApiSetup ConfigureServices(Action<IServicesSetup> setup)
    {
        setup(_servicesSetup);

        return this;
    }

    #endregion

    #region Open up access to local resources

    public IApiSetup AllowAccessToFileSystem()
    {
        _allowAccessToFileSystem = true;

        return this;
    }

    #endregion

    #region Finish configuration, spin up server and hand over control

    public IServerController Done()
    {
        if (_allowAccessToFileSystem)
            _servicesSetup.RegisterSingleton<ITemporaryFileSystem>(new TemporaryFileSystem());

        return _server;
    }

    #endregion

    #region Certificate setup

    public IApiSetup UseSelfSignedCertificate()
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

    public IApiSetup UseCertificate(string storeName, string thumbprint)
    {
        // ToDo - check if store actually exists

        var store = new X509Store(storeName);

        store.Open(OpenFlags.ReadOnly);

        var cert = store
                   .Certificates
                   .FirstOrDefault(c => c.Thumbprint.EqualsThumbprint(thumbprint));

        if (cert == null)
            throw new DeveloperException(
                $"You asked me to supply you with a certificate having this thumbprint: {thumbprint}\n" +
                $"that you assured me was in the store: {storeName}." +
                "Sadly, I could not find it.  Perhaps you mislaid it?");

        store.Close();

        _server.UseCertificate(cert);

        return this;
    }

    #endregion
}