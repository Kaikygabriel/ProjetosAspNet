using System.Collections.Concurrent;

namespace ApiCompras.Logger;

public class LoggingCustomProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, CustomLogger> _cache =
        new ConcurrentDictionary<string, CustomLogger>();

    public void Dispose()
    {
        _cache.Clear();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _cache.GetOrAdd(categoryName, name => new CustomLogger());
    }
}