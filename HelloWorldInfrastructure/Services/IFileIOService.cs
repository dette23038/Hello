//-----------------------------------------------------------------------
// <copyright file="IFileIOService.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldInfrastructure.Services
{
    /// <summary>
    ///     Service for file IO
    /// </summary>
    public interface IFileIOService
    {
        /// <summary>
        ///     Reads the specified file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>The contents of the file</returns>
        string ReadFile(string filePath);
    }
}