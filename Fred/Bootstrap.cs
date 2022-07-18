using Fred.Abstractions.Internal.Services;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations;
using Fred.Implimentations.Http;

namespace Fred;

public static class Bootstrap
{
    private const string INeedAnInstanceOf = "You need to provide me with an instance of {0}.  Without configuration, what am I?";
    private const string HowOldIsYourOs = "Seriously, how old is this operating system?  I cannot work in these conduitions, simply horrendous.";

    public static IApiConfiguration NewServer(string? fileName = null)
    {
        var config = fileName == null
            ? LoadConfig.FromDefault()
            : LoadConfig.FromFile(fileName);

        return NewServer(config);        
    }
    
    public static IApiConfiguration NewServer(IConfig config)
    {
        if(!HttpListener.IsSupported)
            throw new DeveloperException(HowOldIsYourOs);
        
        if(config == null)                            
            throw new DeveloperException(INeedAnInstanceOf, nameof(IConfig));

        var apiServices = new ApiServices();
        
        apiServices.RegisterSingleton<IConfig>(config);
        
        return new ApiConfiguration(
            new Server(apiServices), 
            apiServices, 
            config);
    }
}