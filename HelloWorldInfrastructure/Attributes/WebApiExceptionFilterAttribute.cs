//-----------------------------------------------------------------------
// <copyright file="WebApiExceptionFilterAttribute.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Attributes
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;

    /// <summary>
    ///     Severity code enumeration
    /// </summary>
    public enum SeverityCode
    {
        /// <summary>
        ///     No severity level
        /// </summary>
        None,

        /// <summary>
        ///     Information severity level
        /// </summary>
        Information,

        /// <summary>
        ///     Warning severity level
        /// </summary>
        Warning,

        /// <summary>
        ///     Error severity level
        /// </summary>
        Error,

        /// <summary>
        ///     Critical severity level
        /// </summary>
        Critical
    }

    /// <summary>
    ///     A customer exception filter attribute class for globally handling exceptions and creating a Http Response Message
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        ///     Gets or sets the exception type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        ///     Gets or sets the http status code
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        ///     Gets or sets the severity
        /// </summary>
        public SeverityCode Severity { get; set; }

        /// <summary>
        ///     Gets or sets the logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        ///     Overrides the OnException event that does the work when an exception happens.
        ///     Execution calls this method for all instances of the attribute and again for default error handler
        /// </summary>
        /// <param name="context">The http action context from the http request</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // If the exception type matches
            if (exception.GetType() == this.Type)
            {
                // Get the inner exception message if it exists
                var innerMessage = context.Exception.InnerException != null
                                       ? context.Exception.InnerException.Message
                                       : context.Exception.Message;

                // Create the error response without the stack trace/full exception (It can contain server path information)
                context.Response = context.Request.CreateResponse(
                    this.Status,
                    new ErrorResponseContent
                    {
                        ErrorCode = context.Exception.Message,
                        Message = innerMessage,
                        ExceptionType = context.Exception.GetType().ToString(),
                        FullException = string.Empty,
                        Severity = this.Severity.ToString()
                    });

                // Log the error (including the stack trace/full exception)
                this.Logger.Error(innerMessage, null, context.Exception);
            }
            else
            {
                // Check for an unexpected unhandled exception
                // 1.) The default error handler is being called (this.Type == null)
                // 2.) The response has not already been set (context.Response == null)
                if ((this.Type == null) && (context.Response == null))
                {
                    // Unhandled exception (Critical InternalServerError)
                    context.Response = context.Request.CreateResponse(
                        HttpStatusCode.InternalServerError,
                        new ErrorResponseContent
                        {
                            ErrorCode = ErrorCodes.GeneralError,
                            Message = context.Exception.Message,
                            ExceptionType = context.Exception.GetType().ToString(),
                            FullException = string.Empty,
                            Severity = SeverityCode.Critical.ToString()
                        });

                    // Log the error
                    this.Logger.Error(ErrorCodes.GeneralError, null, context.Exception);
                }
            }
        }
    }
}