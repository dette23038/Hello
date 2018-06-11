//-----------------------------------------------------------------------
// <copyright file="IUri.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.FrameworkWrappers
{
    using System;

    /// <summary>
    ///     Wraps the System.Uri class
    /// </summary>
    public interface IUri
    {
        /// <summary>
        ///     Creates a Uri based on the specified Uri string
        /// </summary>
        /// <param name="uriString">The Uri string</param>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        Uri GetUri(string uriString);
    }
}