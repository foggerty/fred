using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Internal;

namespace Fred;

public static class Bootstrap
{
    public static IApiConfiguration NewServer(string? fileName = null)
    {
        var config = fileName == null
            ? ConfigurationLoader.FromDefault()
            : ConfigurationLoader.FromFile(fileName);

        return NewServer(config);        
    }
    
    public static IApiConfiguration NewServer(IConfiguration configuration)
    {
        if(configuration == null)
        {                               
            throw new DeveloperException($"You need to provide me with an IConfiguration instance.  Without configuration, what am I?");
        }

        var locator = NewServiceLocator(configuration);
        var server = new Server(configuration, locator);
        
        return new ApiConfiguration(server, locator);
    }

    private static IServiceLocatorSetup NewServiceLocator(IConfiguration configuration)
    {
        var locator = new ServiceLocator();
        
        RegistrerDefaultServices(locator, configuration);

        return locator;
    }

    private static void RegistrerDefaultServices(IServiceLocatorSetup locator, IConfiguration configuration)
    {
        // Gloally readable configuration, usually taken from "./fred.config"
        locator.RegisterSingleton<IConfiguration>(() => configuration);

        // The remaining are serives that the server doesn't care about, but are "nice to
        // have" for the various endpoint-handlers.
    }    
}     
