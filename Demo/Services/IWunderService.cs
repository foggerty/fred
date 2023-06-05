using Fred.Abstractions.PublicFacing.Services;

namespace Demo.Services;

public interface IWunderService : IFredService
{
    string WunderName { get; set; }
}