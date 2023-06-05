using Fred.Abstractions.Internal;
using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implementations;

internal class Server : IServerConfiguration
{
    private const string WeRegretToInformYou =
        "We regret to inform you, but at the time of writing this Framework that it's BLOODY TIME TO TURN ALL OF THE HTTP INTO SODDING HTTPS.";

    private const string TangledRoots =
        "You have defined more than one API that uses the root '{0}'.  While it's nice to share, " +
        "we ask that you refrain from doing so in this particular instace, and to instead give each API definition a unique root.";

    private const string SettingMoreThanOneCert =
        "You've already told me to use a certificate, asking me to use another just makes me nervous.  Are we being watched?";

    private const string ApiDefinitionMustHaveEmptyConstructor =
        "An IApiDefinition implementation must have an single (paramaterless) constructor.  No exceptions.  Other than this one.";

    private readonly Dictionary<Type, IApiDefinition> _apis = new();
    private readonly ServiceContainer _endpoints = new();
    private readonly IServices _services;
    private X509Certificate? _certificate;

    public Server(IServices services)
    {
        _services = services;
    }

    public void AddHandler<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>
    {
        AddEndpoint<A, E, Q>();
    }

    public void UseCertificate(X509Certificate certificate)
    {
        if (_certificate != null)
            throw new DeveloperException(SettingMoreThanOneCert);

        _certificate = certificate;
    }

    public void StartApis(TimeSpan timeout)
    {
        //if (_certificate == null)
            //throw new DeveloperException(WeRegretToInformYou);

        // Create Kertrel instance.
        // register endpoint handlers
    }

    public void StopApis(TimeSpan timeout)
    {
        // tell Kestrel to stop receiving requests
        // does it have running stats or just tell it to shutdown and hope?
    }

    public IEnumerable<IApiDefinition> AllApis()
    {
        throw new NotImplementedException();
    }

    private void AddApi<A>()
        where A : IApiDefinition
    {
        var constructor = typeof(A).DefaultConstructorForDi();

        if (constructor == null)
            throw new DeveloperException(ApiDefinitionMustHaveEmptyConstructor);

        var api = (IApiDefinition)constructor.Invoke(null);

        _apis.Add(typeof(A), api);
    }
    
    private void AddEndpoint<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>
    {
        if (!_apis.ContainsKey(typeof(A)))
            AddApi<A>();

        var clashingRoot = _apis
                           .GroupBy(a => a.Value.Root)
                           .Where(g => g.Count() > 1)
                           .FirstOrDefault();

        if (clashingRoot != null)
            throw new DeveloperException(TangledRoots);
    }
}