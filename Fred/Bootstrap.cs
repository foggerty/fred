using Fred.Abstractions.PublicFacing;
using Fred.Implementations;
using Fred.Implementations.Services;

namespace Fred;

public static class Bootstrap
{
    public static IApiSetup NewServer(IConfig config)
    {
        var apiServices = new Services();
        
        apiServices.RegisterSingleton<IConfig>(config);

        return new ApiSetup(new Server(apiServices), apiServices);
    }
}