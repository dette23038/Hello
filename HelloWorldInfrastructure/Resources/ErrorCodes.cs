//-----------------------------------------------------------------------
// <copyright file="ErrorCodes.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Resources
{
    /// <summary>
    /// Contains the error codes for the application
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        ///     The General Error error code
        /// </summary>
        public const string GeneralError = "general-error";

        /// <summary>
        ///     The Today Data File Settings error code
        /// </summary>
        public const string TodaysDataFileSettingsKeyError = "todays-data-file-settings-error";

        /// <summary>
        ///     The Today Data File error code
        /// </summary>
        public const string TodayDataFileError = "today-data-file-error";
    }
}
