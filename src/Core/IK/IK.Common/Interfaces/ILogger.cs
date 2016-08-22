// <copyright file="ILogger.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;

namespace IK.Logging.Interfaces
{
    /// <summary>
    ///     The basic interface for logging information.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Formats and logs the specified message with informational log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// Formats and logs the specified message together with exception with error log level.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        void Error(Exception ex, string format, params object[] args);

        /// <summary>
        /// Formats and logs the specified message with debug log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        void Debug(string format, params object[] args);

        /// <summary>
        /// Formats and logs the specified message with warning log level.
        /// </summary>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        void Warn(string format, params object[] args);

        /// <summary>
        /// Formats and logs the specified message together with exception with warning log level.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="format">The message format.</param>
        /// <param name="args">The format arguments.</param>
        void Warn(Exception ex, string format, params object[] args);
    }
}
