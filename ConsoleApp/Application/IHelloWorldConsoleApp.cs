//-----------------------------------------------------------------------
// <copyright file="IHelloWorldConsoleApp.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleApp.Application
{
    /// <summary>
    ///     Hello World Console Application
    /// </summary>
    public interface IHelloWorldConsoleApp
    {
        /// <summary>
        ///     Runs the main Hello World Console Application
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        void Run(string[] arguments);
    }
}