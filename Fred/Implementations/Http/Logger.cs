namespace Fred.Implementations.Http;

public class NoOpLogger : ILogger, IDisposable
{
    public void Dispose()
    {
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return this;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    public void Log<TState>(LogLevel    logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter)
    {
    }
}

public class NoOpLoggerFactory : ILoggerFactory
{
    private readonly ILogger logger = new NoOpLogger();

    public void Dispose()
    {
    }

    public void AddProvider(ILoggerProvider provider)
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return logger;
    }
}