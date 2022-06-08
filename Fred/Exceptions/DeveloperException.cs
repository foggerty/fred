namespace Fred.Exceptions;

public class DeveloperException : Exception
{
    // The badge of shame!

    // If you get one of these, then something wasn't setup
    // correctly in the API framework, most likely at bootstrap
    // time.

    // Any time Fred gets one of these, he will (after sighing and
    // sadly shaking his head) attempt to shut down the remaining 
    // handlers with a certain timeout, and then kill the main 
    // process (humanely).
    
    public DeveloperException(string message)
        : base(message)
    {
    }

    public DeveloperException(string message, params object[] paramters)
        : base (string.Format(message, paramters))
    {

    }

    public DeveloperException(Exception innerException, string message)
        : base(message, innerException)
    {
    }

    public DeveloperException(Exception innerException, string message, params object[] parameters)
        : base(string.Format(message, parameters), innerException)
    {
    }
}