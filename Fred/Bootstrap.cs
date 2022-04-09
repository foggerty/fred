using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Internal;

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

        var locator = new ServiceLocator();
        var server = new Server(configuration, locator);
        
        return new ApiConfiguration(server, locator);
    }
}     
