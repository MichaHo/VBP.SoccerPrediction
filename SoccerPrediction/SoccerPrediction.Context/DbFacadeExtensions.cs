using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerPrediction.Context
{
    public static class DbFacadeExtensions
    {
        /// <summary>
        /// Configures the database to log all SQL statements
        /// </summary>
        /// <param name="database"> The database facade.</param>
        /// <param name="writeAction">The method to write the log</param>
        public static void Log(this DatabaseFacade database, Action<string> writeAction)
        {

            var loggerFactory = database.GetService<ILoggerFactory>();

            loggerFactory.AddProvider(new DbLoggerProvider(writeAction));
            //loggerFactory.AddConsole(LogLevel.Debug);
        }
    }
}
