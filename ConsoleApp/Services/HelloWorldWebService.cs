//-----------------------------------------------------------------------
// <copyright file="HelloWorldWebService.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleApp.Services
{
    using System;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;
    using RestSharp;

    /// <summary>
    ///     Service class for communicating with the Hello World Web API
    /// </summary>
    public class HelloWorldWebService : IHelloWorldWebService
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly IAppSettings appSettings;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     The Rest client
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        ///     The Rest request
        /// </summary>
        private readonly IRestRequest restRequest;

        /// <summary>
        ///     The wrapped Uri service
        /// </summary>
        private readonly IUri uriService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldWebService" /> class.
        /// </summary>
        /// <param name="restClient">The rest client</param>
        /// <param name="restRequest">The rest request</param>
        /// <param name="appSettings">The application settings</param>
        /// <param name="uriService">The uri service</param>
        /// <param name="logger">The logger</param>
        public HelloWorldWebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }

        /// <summary>
        ///     Gets today's data from the web API
        /// </summary>
        /// <returns>A TodaysData model containing today's data</returns>
        public TodaysData GetTodaysData()
        {
            TodaysData todaysData = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.HelloWorldApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "todaysdata";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var todaysDataResponse = this.restClient.Execute<TodaysData>(this.restRequest);

            // Check for data in the response
            if (todaysDataResponse != null)
            {
                // Check if any actual data was returned
                if (todaysDataResponse.Data != null)
                {
                    todaysData = todaysDataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp, most likely in endpoint URL." + " Error message: "
                                       + todaysDataResponse.ErrorMessage + " HTTP Status Code: "
                                       + todaysDataResponse.StatusCode + " HTTP Status Description: "
                                       + todaysDataResponse.StatusDescription;

                    // Check for existing exception
                    if (todaysDataResponse.ErrorMessage != null && todaysDataResponse.ErrorException != null)
                    {
                        // Log an informative exception including the RestSharp exception
                        this.logger.Error(errorMessage, null, todaysDataResponse.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(todaysDataResponse.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /todaysdata";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return todaysData;
        }
    }
}