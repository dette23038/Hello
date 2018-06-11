//-----------------------------------------------------------------------
// <copyright file="TestLogger.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     This class is for writing log messages, exceptions, and properties to reference variables for use in Unit/Integration Tests
    /// </summary>
    public class TestLogger : ILogger
    {
        /// <summary>
        ///     The list of log messages set by calling classes
        /// </summary>
        private readonly List<string> logMessageList;

        /// <summary>
        ///     The list of exceptions set by calling classes
        /// </summary>
        private readonly List<Exception> exceptionList;

        /// <summary>
        ///     The list of other properties set by calling classes
        /// </summary>
        private readonly List<object> otherPropertiesList;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestLogger"/> class. 
        /// </summary>
        /// <param name="logMessageList">The log message list by reference</param>
        /// <param name="exceptionList">The exception list by reference</param>
        /// <param name="otherPropertiesList">The other properties list by reference</param>
        public TestLogger(ref List<string> logMessageList, ref List<Exception> exceptionList, ref List<object> otherPropertiesList)
        {
            this.logMessageList = logMessageList;
            this.exceptionList = exceptionList;
            this.otherPropertiesList = otherPropertiesList;
        }

        /// <summary>
        ///     Write an INFO message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Info(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }

        /// <summary>
        ///     Write an DEBUG message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        public void Debug(string message, Dictionary<string, object> otherProperties)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
        }

        /// <summary>
        ///     Write an ERROR message to the log
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="otherProperties">Other properties</param>
        /// <param name="exception">Exception instance</param>
        public void Error(string message, Dictionary<string, object> otherProperties, Exception exception)
        {
            this.logMessageList.Add(message);
            this.otherPropertiesList.Add(otherProperties);
            this.exceptionList.Add(exception);
        }
    }
}