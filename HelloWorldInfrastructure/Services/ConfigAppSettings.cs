//-----------------------------------------------------------------------
// <copyright file="ConfigAppSettings.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    using System.Configuration;

    /// <summary>
    ///     Service for application settings in a configuration file
    /// </summary>
    public class ConfigAppSettings : IAppSettings
    {
        /// <summary>
        ///     Gets the string value of a configuration value
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>The application settings value</returns>
        public string Get(string name)
        {
            return ConfigurationManager.AppSettings.Get(name);
        }
    }
}