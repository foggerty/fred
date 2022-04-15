namespace Fred.Exceptions;

public class DeveloperException : Exception
{
    // The badge of shame!

    // If you get one of these, then somethihg wasn't setup
    // correctly in the API framework, most likely at bootstrap
    // time.
    
    public DeveloperException(string? message) : base(message)
    {
    }
}