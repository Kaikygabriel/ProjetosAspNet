
namespace CatalogoApi.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string LoggerName;
        private readonly CustomLoggerProviderConfiguration Configuration;

        public CustomLogger(string loggerName, CustomLoggerProviderConfiguration configuration)
        {
            LoggerName = loggerName;
            Configuration = configuration;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == Configuration.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string menssage = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
            EscreverMenssage(menssage);
        }
        private void EscreverMenssage(string menssage)
        {
            var local = @"C:\Users\kaiky\Downloads\LoggingApiCatalogo.txt";
            using (StreamWriter stream = File.AppendText(local))
            {
                stream.WriteLine(menssage);
            }
        }
    }
}
