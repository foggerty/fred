using Fred.Abstractions.Internal;
using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Functions;

namespace Fred.Implimentations;

internal class Server : IServerConfiguration, IServer
{
    private X509Certificate? _certificate;
    private readonly IApiServices _services;
    private readonly Dictionary<Type, IApiDefinition> _apis = new();

    private const string WeRegretToInformYou = "We regret to inform you, but at the time of writing this Framework it's BLOODY TIME TO TURN ALL HTTP INTO HTTPS.  Ahem.  Excuse me.";
    private const string TangledRoots = "You have defined more than one API that uses the root '{0}'.  While it's nice to share, please give each API definition a unique root.";
    private const string SettingMoreThanOneCert = "You've already told me to use a certificate, asking me to use another just makes me nervous.  Are we being watched?";

    public Server(IApiServices services)
    {
        _services = services;

        // setup Kestrel       
    }

    public void AddHandler<A, E, Q>()
        where A : IApiDefinition
        where E : IApiEndpointHandler<Q>
    {
        AddApi<A>();

        
        // First:
            // Setup Kestrel in seperate console app, link to simple action
            // Add code to do so here
            // Determine what the message format will be.
            // Can then work out how to cleanly map from Kestrel request to one of Fred's handlers.

        // Create a handler function that:
            // gets endpoint via DI
            // asks that endpoint a question
        
        // Add handler function to list of APIs.
        
        // Setup mapping in Kestrel from request to handler function.
    }

    public void UseCertificate(X509Certificate certificate)
    {
        if(_certificate != null)
            throw new DeveloperException(SettingMoreThanOneCert);
        
        _certificate = certificate;
    }

    public void StartApis(TimeSpan timeout)
    {
        if(_certificate == null)
            throw new DeveloperException(WeRegretToInformYou);
    }

    public void StopApis(TimeSpan timeout)
    {

    }

    private void AddApi<A>()
        where A : IApiDefinition
    {
        if(_apis.ContainsKey(typeof(A)))
            return;

        var constructor = typeof(A).EmptyConstructor();

        if(constructor == null)
            throw new DeveloperException("");
        
        var api = (IApiDefinition)constructor.Invoke(null);

        _apis.Add(typeof(A), api);

        var clashingRoot = _apis
            .GroupBy(a => a.Value.Root)
            .Where(g => g.Count() > 1)
            .FirstOrDefault();
        
        if(clashingRoot != null)
            throw new DeveloperException(TangledRoots);
    }
}