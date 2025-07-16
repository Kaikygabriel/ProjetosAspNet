namespace ApiCompras.Logger;

public class CustomLogger : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;    
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == LogLevel.Warning;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string menssage = $"Log level :{logLevel.ToString()}, event id : {eventId.Id}";
        EnviarMenssage(menssage);
    }

    public void EnviarMenssage(string menssage)
    {
        using (StreamWriter sw = File.AppendText(@"C:\Users\kaiky\Downloads\LoggingCompras.txt"))
        {
            sw.WriteLine(menssage);
        }
    }
}