//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Service for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Write an INFO message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        void Info(string message, Dictionary<string, object> otherProperties);

        /// <summary>
        ///     Write an DEBUG message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        void Debug(string message, Dictionary<string, object> otherProperties);

        /// <summary>
        ///     Write an ERROR message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        void Error(string message, Dictionary<string, object> otherProperties, Exception exception);
    }
}