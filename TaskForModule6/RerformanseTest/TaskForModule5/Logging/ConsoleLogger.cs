using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TaskForModule5.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, 
            EventId eventId, TState state,
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            Console.WriteLine(message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return ConsoleLogScope.Push(nameof(ConsoleLogger), state);
        }
    }
}