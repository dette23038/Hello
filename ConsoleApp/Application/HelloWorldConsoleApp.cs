//-----------------------------------------------------------------------
// <copyright file="HelloWorldConsoleApp.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleApp.Application
{
    using ConsoleApp.Services;
    using HelloWorldInfrastructure.Services;

    /// <summary>
    ///     Hello World Console Application
    /// </summary>
    public class HelloWorldConsoleApp : IHelloWorldConsoleApp
    {
        /// <summary>
        ///     The Hello World Web Service
        /// </summary>
        private readonly IHelloWorldWebService helloWorldWebService;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldConsoleApp" /> class.
        /// </summary>
        /// <param name="helloWorldWebService">The injected hello world web service</param>
        /// <param name="logger">The logger</param>
        public HelloWorldConsoleApp(IHelloWorldWebService helloWorldWebService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldWebService;
            this.logger = logger;
        }

        /// <summary>
        ///     Runs the main Hello World Console Application
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        public void Run(string[] arguments)
        {
            // Get Today's data
            var todaysData = this.helloWorldWebService.GetTodaysData();

            // Write Today's data to the screen
            this.logger.Info(todaysData != null ? todaysData.Data : "No data was found!", null);
        }
    }
}