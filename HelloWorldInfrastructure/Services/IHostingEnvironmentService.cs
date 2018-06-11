//-----------------------------------------------------------------------
// <copyright file="IHostingEnvironmentService.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    /// <summary>
    ///     Service for Hosting Environment
    /// </summary>
    public interface IHostingEnvironmentService
    {
        /// <summary>
        ///     Map's the specified path to the hosting environment's path
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The hosting environment's path</returns>
        string MapPath(string path);
    }
}