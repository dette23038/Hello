//-----------------------------------------------------------------------
// <copyright file="SystemDateTime.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.FrameworkWrappers
{
    using System;

    /// <summary>
    ///     Wraps the System.DateTime structure
    /// </summary>
    public class SystemDateTime : IDateTime
    {
        /// <summary>
        ///     Gets the DateTime as of Now
        /// </summary>
        /// <returns>A DateTime object containing the date and time of Now</returns>
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}