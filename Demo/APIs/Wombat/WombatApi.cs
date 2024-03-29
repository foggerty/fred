using Fred.Abstractions.PublicFacing;

namespace Demo.APIs.Wombat;

public record WombatApi : IApiDefinition
{
    public string Name => "Wombat API";

    public string Description => "For all your wombat needs!";

    public Version Version => new(1, 0, 0, 0);

    public string Root => "wombats";
}