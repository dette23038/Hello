//-----------------------------------------------------------------------
// <copyright file="SystemUri.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.FrameworkWrappers
{
    using System;

    /// <summary>
    ///     Wraps the System.URI class
    /// </summary>
    public class SystemUri : IUri
    {
        /// <summary>
        ///     Creates a Uri based on the specified Uri string
        /// </summary>
        /// <param name="uriString">The Uri string</param>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        public Uri GetUri(string uriString)
        {
            return new Uri(uriString);
        }
    }
}