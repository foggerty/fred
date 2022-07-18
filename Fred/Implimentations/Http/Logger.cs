namespace Fred.Implimentations.Http;

sealed class NoOpLogger : ILogger, IDisposable
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return this;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
    }

    public void Dispose()
    {
    }
}

sealed class NoOpLoggerFactory : ILoggerFactory
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
        return this.logger;
    }
}