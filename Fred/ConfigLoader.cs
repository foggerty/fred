using System.Text;
using System.Text.Json;
using Fred.Abstractions.Internal;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;

namespace Fred;

internal static class ConfigurationLoader
{
    internal const string DEFAULT_CONFIG = "fred.config";

    internal static IConfiguration FromDefault()
    {
        var pwd = Environment.CurrentDirectory;
        var path = Path.Combine(pwd, DEFAULT_CONFIG);

        if(File.Exists(path))
            return LoadConfig(path);
        
        return DefaultConfig();
    }
    
    internal static IConfiguration FromFile(string? fileName)
    {
        if(!File.Exists(fileName))
            throw new DeveloperException($"The file '{fileName}' is nowhere to be found.  Maybe it never existed?");
        
        return LoadConfig(fileName);
    }         

    private static IConfiguration LoadConfig(string path)
    {
        var asbytes = File.ReadAllBytes(path);
        var asString = Encoding.UTF8.GetString(asbytes);

        try
        {
            var configuration = JsonSerializer.Deserialize<Configuration>(asString);

            if(configuration == null)
                throw new DeveloperException($"The configuration at {path} was not what it first seemed...");

            return configuration;
        }
        catch
        {
            throw new DeveloperException($"There's something horribly, horrifyingly wrong with {path}");
        }        
    }

    private static IConfiguration DefaultConfig()
    {
        throw new NotImplementedException();
    }
}