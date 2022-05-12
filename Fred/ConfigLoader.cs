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
        
        return new Config();
    }
    
    internal static IConfig FromFile(string? fileName)
    {
        if(!File.Exists(fileName))
            throw new DeveloperException($"The file '{fileName}' is nowhere to be found.  Maybe it never existed?");
        
        return Load(fileName);
    }         

    private static IConfig Load(string path)
    {
        var config = new Config();

        config.ReadFromFile(path);

        return config;
    }
}