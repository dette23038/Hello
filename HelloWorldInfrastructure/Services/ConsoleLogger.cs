//-----------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HelloWorldInfrastructure.FrameworkWrappers;

    /// <summary>
    ///     Service for logging to the Console window
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        ///     The Console abstraction for writing to the console.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConsoleLogger" /> class.
        /// </summary>
        /// <param name="console">The injected console</param>
        public ConsoleLogger(IConsole console)
        {
            this.console = console;
        }

        /// <summary>
        ///     Write an INFO message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("INFO", message, otherProperties, null);
        }

        /// <summary>
        ///     Write an DEBUG message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog("DEBUG", message, otherProperties, null);
        }

        /// <summary>
        ///     Write an ERROR message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog("ERROR", message, otherProperties, exception);
        }

        /// <summary>
        ///     Writes the log level to the Console
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        private void WriteLog(string logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create a string builder with the log level and message
            var builder = new StringBuilder(logLevel);
            builder.Append(": ");
            builder.Append(message);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        builder.Append(" [");
                        builder.Append(property.Key);
                        builder.Append("=");
                        builder.Append(property.Value);
                        builder.Append("]");
                    }
                }
            }

            // Check for an exception
            if (exception != null)
            {
                builder.Append(" [Exception: ");
                builder.Append(exception);
                builder.Append("]");
            }

            // Write the log to the Console
            this.console.WriteLine(builder.ToString());
        }
    }
}