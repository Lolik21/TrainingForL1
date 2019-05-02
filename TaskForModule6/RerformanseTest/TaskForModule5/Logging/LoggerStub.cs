using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TaskForModule5.Logging
{
    public class LoggerStub: ILogger
    {
        public void Log<TState>(
            LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return ConsoleLogScope.Push(nameof(LoggerStub), state);
        }
    }
}