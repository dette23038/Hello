//-----------------------------------------------------------------------
// <copyright file="MainDriver.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleApp.Application
{
    using ConsoleApp.Services;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Services;
    using LightInject;
    using RestSharp;

    /// <summary>
    ///     Main class that drives the application
    /// </summary>
    public class MainDriver
    {
        /// <summary>
        ///     Starts the console application with the specified command line arguments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            // Setup dependency injection and run the application
            using (var container = new ServiceContainer())
            {
                // Configure depenency injection
                container.Register<IHelloWorldConsoleApp, HelloWorldConsoleApp>();
                container.Register<IAppSettings, ConfigAppSettings>();
                container.Register<IConsole, SystemConsole>();
                container.Register<ILogger, ConsoleLogger>();
                container.Register<IUri, SystemUri>();
                container.Register<IHelloWorldWebService, HelloWorldWebService>();
                container.RegisterInstance(typeof(IRestClient), new RestClient());
                container.RegisterInstance(typeof(IRestRequest), new RestRequest());

                // Run the main program
                container.GetInstance<IHelloWorldConsoleApp>().Run(args);
            }
        }
    }
}