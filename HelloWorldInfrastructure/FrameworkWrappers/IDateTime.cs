//-----------------------------------------------------------------------
// <copyright file="IDateTime.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.FrameworkWrappers
{
    using System;

    /// <summary>
    ///     Wraps the DateTime structure
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        ///     Gets the DateTime as of Now
        /// </summary>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        DateTime Now();
    }
}