using System.Collections.Concurrent;

namespace CatalogoApi.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomLoggerProviderConfiguration loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> customLoggerCache=
                                            new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration logger)
        {
            loggerConfig = logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return customLoggerCache.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            customLoggerCache.Clear();
        }
    }
}
