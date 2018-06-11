//-----------------------------------------------------------------------
// <copyright file="HelloWorldMapperUnitTests.cs" company="Ryan Woodcox">
//  Copyright (c) 2015 All Rights Reserved
//  <author>Ryan Woodcox</author>
// </copyright>
//-----------------------------------------------------------------------

namespace HelloWorldAPI.Tests.UnitTests
{
    using HelloWorldInfrastructure.Mappers;
    using HelloWorldInfrastructure.Models;
    using NUnit.Framework;

    /// <summary>
    ///     Unit tests for the Hello World Mapper
    /// </summary>
    [TestFixture]
    public class HelloWorldMapperUnitTests
    {
        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldMapper helloWorldMapper;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Create object to test
            this.helloWorldMapper = new HelloWorldMapper();
        }

        #region StringToTodaysData Tests
        /// <summary>
        ///     Tests the class's StringToTodaysData method for success with a normal input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNormalSuccess()
        {
            const string Data = "Hello World!";

            // Create the expected result
            var expectedResult = GetSampleTodaysData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToTodaysData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        /// <summary>
        ///     Tests the StringToTodaysData method for success with a null input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperStringToTodaysDataNullSuccess()
        {
            const string Data = null;

            // Create the expected result
            var expectedResult = GetSampleTodaysData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToTodaysData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
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
            return new TodaysData()
            {
                Data = data
            };
        }
        #endregion
    }
}