// <copyright file="Logger.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using IK.Logging.Interfaces;
using log4net;
using log4net.Config;

namespace IK.Logging.Implementation
{
    /// <summary>
    ///     The implementation of logger.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(typeof(Logger));

        /// <summary>
        /// Initializes static members of the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Formats and logs the specified message with informational log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        public void Info(string format, params object[] args)
        {
            this.logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Formats and logs the specified message together with exception with error log level.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        public void Error(Exception ex, string format, params object[] args)
        {
            this.logger.Error(string.Format(format, args), ex);
        }

        /// <summary>
        /// Formats and logs the specified message with debug log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        public void Debug(string format, params object[] args)
        {
            this.logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Formats and logs the specified message with warning log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        public void Warn(string format, params object[] args)
        {
            this.logger.WarnFormat(format, args);
        }

        /// <summary>
        /// Formats and logs the specified message together with exception with warning log level.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        public void Warn(Exception ex, string format, params object[] args)
        {
            this.logger.Warn(string.Format(format, args), ex);
        }
    }
}
