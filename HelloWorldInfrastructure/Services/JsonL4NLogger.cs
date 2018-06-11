//-----------------------------------------------------------------------
// <copyright file="JsonL4NLogger.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using log4net.Config;
    using log4net.Core;

    /// <summary>
    ///     Logger class that uses the Log4Net library
    /// </summary>
    public class JsonL4NLogger : ILogger
    {
        /// <summary>
        ///     The log4net logger
        /// </summary>
        private readonly log4net.Core.ILogger log4NetLogger;

        /// <summary>
        ///     The logger name
        /// </summary>
        private string loggerName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonL4NLogger" /> class.
        /// </summary>
        public JsonL4NLogger()
        {
            XmlConfigurator.Configure();
            this.log4NetLogger = LoggerManager.GetLogger(this.GetType().Assembly, this.GetType().Name);
            ////this.log4NetLogger = LoggerManager.GetLogger(this.GetType().Assembly, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            this.loggerName = this.GetType().Name;
        }

        /// <summary>
        ///     Write an INFO message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Info, message, otherProperties, null);
        }

        /// <summary>
        ///     Write an DEBUG message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.WriteLog(Level.Debug, message, otherProperties, null);
        }

        /// <summary>
        ///     Write an ERROR message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.WriteLog(Level.Error, message, otherProperties, exception);
        }

        /// <summary>
        ///     Writes the log using log4net
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        private void WriteLog(Level logLevel, string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            // Create the logging event data
            var loggingEventData = new LoggingEventData()
            {
                Level = logLevel,
                LoggerName = this.loggerName,
                Domain = AppDomain.CurrentDomain.FriendlyName,
                TimeStamp = DateTime.Now,
                Message = message
            };

            // Create the logging event
            var loggingEvent = new LoggingEvent(loggingEventData);

            // Check for other properties
            if (otherProperties != null)
            {
                foreach (var property in otherProperties)
                {
                    if (property.Key != null && property.Value != null)
                    {
                        loggingEvent.Properties[property.Key] = property.Value;
                    }
                }
            }

            // Check for exception
            if (exception != null)
            {
                loggingEvent.Properties["exception"] = exception.ToString();
            }

            // Log the data
            this.log4NetLogger.Log(loggingEvent);
        }
    }
}
