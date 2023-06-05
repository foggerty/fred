using Fred.Abstractions.PublicFacing.Services;

namespace Demo.Services;

public class WunderService : IWunderService
{
    public WunderService(string name)
    {
        WunderName = name;
    }
    
    public string WunderName { get; set; }
}