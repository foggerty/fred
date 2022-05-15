using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations;
using Fred.Implimentations.Services;

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
    
    public static IApiConfiguration NewServer(IConfig configuration)
    {
        if(configuration == null)
        {                               
            throw new DeveloperException($"You need to provide me with an instance of {nameof(IConfig)}.  Without configuration, what am I?");
        }

        var locator = NewServiceLocator(configuration);
        var server = new Server(configuration, locator);
        
        return new ApiConfiguration(server, locator);
    }

    internal static IServiceLocatorSetup NewServiceLocator(IConfig config)
    {
        var locator = new ServiceLocator();
        
        // Add internal and default services that all APIs can use.

        locator.RegisterSingleton<IConfig>(config);  // intrenal use

        return locator;
    }
}