//-----------------------------------------------------------------------
// <copyright file="HelloWorldWebServiceUnitTests.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldAPI.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using ConsoleApp.Services;
    using HelloWorldInfrastructure.FrameworkWrappers;
    using HelloWorldInfrastructure.Models;
    using HelloWorldInfrastructure.Resources;
    using HelloWorldInfrastructure.Services;
    using Moq;
    using NUnit.Framework;
    using RestSharp;

    /// <summary>
    ///     Unit tests for the Hello World Console App
    /// </summary>
    [TestFixture]
    public class HelloWorldWebServiceUnitTests
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
        ///     The mocked application settings service
        /// </summary>
        private Mock<IAppSettings> appSettingsMock;

        /// <summary>
        ///     The test logger
        /// </summary>
        private ILogger testLogger;

        /// <summary>
        ///     The mocked Rest client
        /// </summary>
        private Mock<IRestClient> restClientMock;

        /// <summary>
        ///     The mocked Rest request
        /// </summary>
        private Mock<IRestRequest> restRequestMock;

        /// <summary>
        ///     The mocked wrapped Uri service
        /// </summary>
        private Mock<IUri> uriServiceMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldWebService helleHelloWorldWebService;

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
            this.appSettingsMock = new Mock<IAppSettings>();
            this.testLogger = new TestLogger(ref this.logMessageList, ref this.exceptionList, ref this.otherPropertiesList);
            this.restClientMock = new Mock<IRestClient>();
            this.restRequestMock = new Mock<IRestRequest>();
            this.uriServiceMock = new Mock<IUri>();

            // Create object to test
            this.helleHelloWorldWebService = new HelloWorldWebService(
                this.restClientMock.Object,
                this.restRequestMock.Object,
                this.appSettingsMock.Object,
                this.uriServiceMock.Object,
                this.testLogger);
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

        #region GetTodaysData Tests
        /// <summary>
        ///     Tests the class's GetTodaysData method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            var todaysData = GetSampleTodaysData(Data);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.NotNull(response);
            Assert.AreEqual(response.Data, todaysData.Data);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is a null response
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullResponse()
        {
            // Create return models for dependencies
            const string Data = "Hello World!";
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = (IRestResponse<TodaysData>)null;
            var todaysData = GetSampleTodaysData(Data);
            const string ErrorMessage = "Did not get any response from the Hello World Web Api for the Method: GET /todaysdata";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], ErrorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ErrorMessage);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is null data in the response
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullData()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL." 
                + " Error message: " + ErrorMessage 
                + " HTTP Status Code: " + StatusCode 
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, errorException.Message);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is null data in the response and a null error message
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorMessage()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = null;
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            var errorException = new Exception("errorHere");
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
        }

        /// <summary>
        ///     Tests the class's GetTodaysData method for success when there is null data in the response and a null error exception
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataNullDataNullErrorException()
        {
            // Create return models for dependencies
            const string WebApiIUrl = "http://www.somesiteheretesting.com";
            var uri = new Uri(WebApiIUrl);
            var mockParameters = new Mock<List<Parameter>>();
            var mockRestResponse = new Mock<IRestResponse<TodaysData>>();
            TodaysData todaysData = null;
            const string ErrorMessage = "Error Message";
            const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            const string StatusDescription = "Status Description";
            Exception errorException = null;
            const string ProfileContent = "Content here";

            var errorMessage = "Error in RestSharp, most likely in endpoint URL."
                + " Error message: " + ErrorMessage
                + " HTTP Status Code: " + StatusCode
                + " HTTP Status Description: " + StatusDescription;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldApiUrlKey)).Returns(WebApiIUrl);
            this.uriServiceMock.Setup(m => m.GetUri(WebApiIUrl)).Returns(uri);
            this.restRequestMock.Setup(m => m.Parameters).Returns(mockParameters.Object);
            this.restClientMock.Setup(m => m.Execute<TodaysData>(It.IsAny<IRestRequest>())).Returns(mockRestResponse.Object);
            mockRestResponse.Setup(m => m.Data).Returns(todaysData);
            mockRestResponse.Setup(m => m.ErrorMessage).Returns(ErrorMessage);
            mockRestResponse.Setup(m => m.StatusCode).Returns(StatusCode);
            mockRestResponse.Setup(m => m.StatusDescription).Returns(StatusDescription);
            mockRestResponse.Setup(m => m.ErrorException).Returns(errorException);
            mockRestResponse.Setup(m => m.Content).Returns(ProfileContent);

            // Call the method to test
            var response = this.helleHelloWorldWebService.GetTodaysData();

            // Check values
            Assert.IsNull(response);
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], errorMessage);
            Assert.AreEqual(this.exceptionList.Count, 1);
            Assert.AreEqual(this.exceptionList[0].Message, ProfileContent);
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