using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Internal;
using Fred.Implimentations.Internal.Services;

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
        
        return new ApIServerSettings(server, locator);
    }

    internal static IServiceLocatorSetup NewServiceLocator(IConfig configuration)
    {
        var locator = new ServiceLocator();
        
        RegistrerDefaultServices(locator, configuration);

        return locator;
    }

    internal static WebApplication NewWebApp(IConfig settings)
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
        
        return app;
    }
    
    internal static void RegistrerDefaultServices(IServiceLocatorSetup locator, IConfig configuration)
    {
        // Gloally readable configuration, usually taken from "./fred.config"
        locator.RegisterSingleton<IConfig>(() => configuration);

        // The remaining are serives that the server doesn't care about, but are "nice to
        // have" for the various endpoint-handlers.
    }     
}     
