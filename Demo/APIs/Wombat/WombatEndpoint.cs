using Demo.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Functions;

namespace Demo.APIs.Wombat;

public class WombatEndpoint : IApiEndpointHandler<string>
{
    private readonly ITemporaryFileSystem _tempFileSystem;
    private readonly IWunderService _wunderService;

    public WombatEndpoint(
        ITemporaryFileSystem tempFileSystem,
        IWunderService wunderService)
    {
        _tempFileSystem = tempFileSystem;
        _wunderService  = wunderService;
    }

    public string Path => _tempFileSystem.IsAvailable()
        ? "Available"
        : "Nothing to see here.";

    public Func<string, IAnswer> Handler => x =>
        int.Parse(x)
           .ToAnswer();
}