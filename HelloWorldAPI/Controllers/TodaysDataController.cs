//-----------------------------------------------------------------------
// <copyright file="TodaysDataController.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldAPI.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Web.Http;
    using HelloWorldInfrastructure.Attributes;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Services;

    /// <summary>
    ///     API controller for getting and setting today's value.
    /// </summary>
    [WebApiExceptionFilter]
    public class TodaysDataController : ApiController
    {
        /// <summary>
        ///     The data service
        /// </summary>
        private readonly IDataService dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodaysDataController" /> class.
        /// </summary>
        /// <param name="dataService">The injected data service</param>
        public TodaysDataController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        ///     Gets today's value
        /// </summary>
        /// <returns>A TodaysData model containing today's value</returns>
        [WebApiExceptionFilter(Type = typeof(IOException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        [WebApiExceptionFilter(Type = typeof(SettingsPropertyNotFoundException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        public TodaysData Get()
        {
            return this.dataService.GetTodaysData();
        }
    }
}