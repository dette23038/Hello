//-----------------------------------------------------------------------
// <copyright file="IAppSettings.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    /// <summary>
    ///     Service for application settings
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        ///     Gets the string value of a configuration value
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>The application settings value</returns>
        string Get(string name);
    }
}