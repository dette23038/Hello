//-----------------------------------------------------------------------
// <copyright file="ServerHostingEnvironmentService.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System.Web.Hosting;

    /// <summary>
    ///     Service for Server Hosting Environment
    /// </summary>
    public class ServerHostingEnvironmentService : IHostingEnvironmentService
    {
        /// <summary>
        ///     Map's the specified path to the hosting environment's path
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The hosting environment's path</returns>
        public string MapPath(string path)
        {
            return HostingEnvironment.MapPath("~/" + path);
        }
    }
}