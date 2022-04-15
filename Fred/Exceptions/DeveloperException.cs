namespace Fred.Exceptions;

public class DeveloperException : Exception
{
    // The badge of shame!

    // If you get one of these, then somethihg wasn't setup
    // correctly in the API framework, most likely at bootstrap
    // time.

    // Any time Fred gets one of these, he will (after sighing and
    // sadly shaking his head) attempt to shut down the remaining 
    // handlers with a certain timeout, and then kill the main 
    // process (humanely).
    
    public DeveloperException(string? message) : base(message)
    {
    }
}