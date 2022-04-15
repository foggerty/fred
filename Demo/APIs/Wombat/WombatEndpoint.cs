using System.Net;
using Fred.Abstractions.PublicFacing;
using Fred.Functions;

namespace Demo.APIs.Wombat;

public class WombatEndpoint : IApiEndpointHandler<string>
{
    public string Path => "dostuff";

    public Func<string, IAnswer> Handler => x =>
        int.Parse(x).ToAnswer(HttpStatusCode.OK);
}

