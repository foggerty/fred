using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations;

namespace Fred;

public static class Bootstrap
{
    public static IApiConfiguration NewServer(string? fileName = null)
    {
        var config = fileName == null
            ? LoadConfig.FromDefault()
            : LoadConfig.FromFile(fileName);

        return NewServer(config);        
    }
    
    public static IApiConfiguration NewServer(IConfig config)
    {
        if(config == null)
        {                               
            throw new DeveloperException($"You need to provide me with an instance of {nameof(IConfig)}.  Without configuration, what am I?");
        }

        var apiServices = new ApiServices();
        
        apiServices.RegisterSingleton<IConfig>(config);

        var server = new Server(apiServices);
        
        return new ApiConfiguration(server, apiServices, config);
    }
}