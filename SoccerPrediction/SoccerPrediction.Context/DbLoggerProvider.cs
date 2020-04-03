using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerPrediction.Context
{
    internal class DbLoggerProvider : ILoggerProvider
    {
        private readonly Action<string> _writeAction;

        public DbLoggerProvider(Action<string> writeAction)
        {
            _writeAction = writeAction;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DbSimpleLogger(_writeAction);
        }

        public void Dispose()
        {
        }
    }
}
