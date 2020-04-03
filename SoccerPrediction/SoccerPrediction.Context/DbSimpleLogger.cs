using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerPrediction.Context
{
    internal class DbSimpleLogger : ILogger
    {
        private readonly Action<string> _writeAction;

        public DbSimpleLogger(Action<string> writeAction)
        {
            _writeAction = writeAction;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = $"{Environment.NewLine}{formatter(state, exception)}";
            _writeAction(message);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

    }
}
