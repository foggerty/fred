using System.Data;
using System.Net.Http.Headers;
using System.Security;
using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Abstractions.PublicFacing.Services;
using Fred.Exceptions;
using Fred.Implimentations.Internal;
using Fred.Implimentations.Internal.Services;

namespace Fred;

public static class Bootstrap
{
    public static IConfiguration ReadConfiguration()
    {
        throw new NotImplementedException();
    }
    
    public static IApiConfiguration NewServer(IConfiguration configuration)
    {
        if(configuration == null)
        {                               
            throw new DeveloperException($"You need to provide me with an IConfiguration instance.  Without configuration, what am I?");
        }

        var locator = ServiceLocator();
        var server = new Server(configuration, (ServiceLocator)locator);
        
        return new ApiConfiguration(server, locator);
    }

    public static IServiceLocatorSetup ServiceLocator()
    {
        var locator = new ServiceLocator();
        
        // Add standard services, available to all APIs.        

        locator.RegisterSingleton<IObjectStore>(() => new ObjectStore());

        return locator;
    }
}     
