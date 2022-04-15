namespace Fred.Exceptions;

public class ConfigurationException : Exception
{
    // If you get this, then something in the API's configuration is messed up.

    // Blame the junior dev that you got to set it up.

    public ConfigurationException(string? message) : base(message)
    {            
    }
}