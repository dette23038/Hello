//-----------------------------------------------------------------------
// <copyright file="HelloWorldConsoleAppUnitTests.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldAPI.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using ConsoleApp.Application;
    using ConsoleApp.Services;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Console App
    /// </summary>
    [TestFixture]
    public class HelloWorldConsoleAppUnitTests
    {
        /// <summary>
        ///     The list of log messages set by calling classes
        /// </summary>
        private List<string> logMessageList;

        /// <summary>
        ///     The list of exceptions set by calling classes
        /// </summary>
        private List<Exception> exceptionList;

        /// <summary>
        ///     The list of other properties set by calling classes
        /// </summary>
        private List<object> otherPropertiesList;

        /// <summary>
        ///     The mocked Hello World Web Service
        /// </summary>
        private Mock<IHelloWorldWebService> helloWorldWebServiceMock;

        /// <summary>
        ///     The test logger
        /// </summary>
        private ILogger testLogger;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldConsoleApp helloWorldConsoleApp;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.helloWorldWebServiceMock = new Mock<IHelloWorldWebService>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);

            // Create object to test
            this.helloWorldConsoleApp = new HelloWorldConsoleApp(this.helloWorldWebServiceMock.Object, this.testLogger);
        }

        /// <summary>
        ///     Test tear down. (runs after each test)
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        #region Run Tests
        /// <summary>
        ///     Tests the class's Run method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!";

            // Create return models for dependencies
            var todaysData = GetSampleTodaysData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetTodaysData()).Returns(todaysData);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }

        /// <summary>
        ///     Tests the class's Run method for success when null data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetTodaysData()).Returns((TodaysData)null);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
        }
        #endregion
        
        #region Helper Methods
        /// <summary>
        ///     Gets a sample TodaysData model
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>A sample TodaysData model</returns>
        private static TodaysData GetSampleTodaysData(string data)
        {
            return new TodaysData { Data = data };
        }
        #endregion
    }
}