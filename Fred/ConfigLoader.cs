using System.Text;
using System.Text.Json;
using Fred.Abstractions.PublicFacing;
using Fred.Exceptions;
using Fred.Implimentations.Internal;

namespace Fred;

internal static class LoadConfig
{
    internal const string DEFAULT_CONFIG = "fred.config";

    internal static IConfig FromDefault()
    {
        var pwd = AppDomain.CurrentDomain.BaseDirectory;
        var path = Path.Combine(pwd, DEFAULT_CONFIG);

        if(File.Exists(path))
            return Load(path);
        
        return DefaultConfig();
    }
    
    internal static IConfig FromFile(string? fileName)
    {
        if(!File.Exists(fileName))
            throw new DeveloperException($"The file '{fileName}' is nowhere to be found.  Maybe it never existed?");
        
        return Load(fileName);
    }         

    private static IConfig Load(string path)
    {
        var asbytes = File.ReadAllBytes(path);
        var asString = Encoding.UTF8.GetString(asbytes);
        Config? configuration;

        try
        {
            configuration = JsonSerializer.Deserialize<Config>(asString);
        }
        catch(Exception ex)
        {
            throw new DeveloperException("I am sorry to be the one to report this, but your configuration file is simply not up to scratch.", ex);
        }

        if(configuration == null)
            throw new DeveloperException($"The configuration at {path} was not what I thought, but it was in fact...  The Abyss!");

        return configuration;
    }

    private static IConfig DefaultConfig()
    {
        throw new Exception("Not sure if should allow this - should always insist on config?");
    }
}