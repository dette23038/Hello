//-----------------------------------------------------------------------
// <copyright file="IHelloWorldWebService.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleApp.Services
{
    using HelloWorldInfrastructure.Models;

    /// <summary>
    ///     Service for communicating with the Hello World Web API
    /// </summary>
    public interface IHelloWorldWebService
    {
        /// <summary>
        ///     Gets today's data from the web API
        /// </summary>
        /// <returns>A TodaysData model containing today's data</returns>
        TodaysData GetTodaysData();
    }
}