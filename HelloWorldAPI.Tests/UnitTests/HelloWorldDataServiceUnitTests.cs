//-----------------------------------------------------------------------
// <copyright file="HelloWorldDataServiceUnitTests.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldAPI.Tests.UnitTests
{
    using System;
    using System.Configuration;
    using System.IO;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Mappers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Data Service
    /// </summary>
    [TestFixture]
    public class HelloWorldDataServiceUnitTests
    {
        /// <summary>
        ///     The mocked application settings service
        /// </summary>
        private Mock<IAppSettings> appSettingsMock;

        /// <summary>
        ///     The mocked DateTime wrapper
        /// </summary>
        private Mock<IDateTime> dateTimeWrapperMock;

        /// <summary>
        ///     The mocked File IO service
        /// </summary>
        private Mock<IFileIOService> fileIOServiceMock;

        /// <summary>
        ///     The mocked Hello World Mapper
        /// </summary>
        private Mock<IHelloWorldMapper> helloWorldMapperMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldDataService helloWorldDataService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.dateTimeWrapperMock = new Mock<IDateTime>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IHelloWorldMapper>();

            // Create object to test
            this.helloWorldDataService = new HelloWorldDataService(
                this.appSettingsMock.Object,
                this.dateTimeWrapperMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        #region GetTodaysData Tests
        /// <summary>
        ///     Tests the class's GetTodaysData method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSuccess()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";
            const string FileContents = "Hello World!";
            var nowDate = DateTime.Now;
            var rawData = FileContents + " as of " + nowDate.ToString("F");

            // Create the expected result
            var expectedResult = GetSampleTodaysData(rawData);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Returns(FileContents);
            this.dateTimeWrapperMock.Setup(m => m.Now()).Returns(nowDate);
            this.helloWorldMapperMock.Setup(m => m.StringToTodaysData(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataService.GetTodaysData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method when the setting key is null
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.TodaysDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSettingKeyNull()
        {
            // Create return models for dependencies
            const string DataFilePath = null;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);

            // Call the method to test
            this.helloWorldDataService.GetTodaysData();
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method when the setting key is an empty string
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.TodaysDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetTodaysDataSettingKeyEmptyString()
        {
            // Create return models for dependencies
            var dataFilePath = string.Empty;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(dataFilePath);

            // Call the method to test
            this.helloWorldDataService.GetTodaysData();
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for an IO Exception
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestHelloWorldDataServiceGetTodaysDataIOException()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.TodayDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Throws(new IOException("Error!"));

            // Call the method to test
            this.helloWorldDataService.GetTodaysData();
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