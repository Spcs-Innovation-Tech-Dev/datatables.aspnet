﻿using DataTables.AspNet.AspNetCore.Tests.Mocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'DataTablesResponse' class.
    /// </summary>
    public class DataTablesResponseTests
    {
        /// <summary>
        /// Validates error response creation.
        /// </summary>
        [Fact]
        public void ErrorResponse()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);

            // Act
            var response = DataTablesResponse<MockData>.Create(request, "just_an_error_message");

            // Assert
            Assert.Equal(request.Draw, response.Draw);
            Assert.Equal("just_an_error_message", response.Error);
            Assert.Equal(0, response.TotalRecords);
            Assert.Equal(0, response.TotalRecordsFiltered);
            Assert.Null(response.Data);
            Assert.Null(response.AdditionalParameters);
        }
        /// <summary>
        /// Validates response creation without aditional parameters dictionary.
        /// </summary>
        [Fact]
        public void ResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data);

            // Assert
            Assert.Equal(request.Draw, response.Draw);
            Assert.Null(response.Error);
            Assert.Equal(2000, response.TotalRecords);
            Assert.Equal(1000, response.TotalRecordsFiltered);
            Assert.Equal(data, response.Data);
            Assert.Null(response.AdditionalParameters);
        }
        /// <summary>
        /// Validates response creation with aditional parameters dictionary.
        /// </summary>
        [Fact]
        public void ResponseWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var aditionalParameters = TestHelper.MockAdditionalParameters();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data, aditionalParameters);

            // Assert
            Assert.Equal(request.Draw, response.Draw);
            Assert.Null(response.Error);
            Assert.Equal(2000, response.TotalRecords);
            Assert.Equal(1000, response.TotalRecordsFiltered);
            Assert.Equal(data, response.Data);
            Assert.Equal(aditionalParameters, response.AdditionalParameters);
        }
        /// <summary>
        /// Validates error response serialization.
        /// </summary>
        //[Fact]
        //public void ErrorResponseSerializationWithoutAditionalParameters()
        //{
        //    // Arrange
        //    var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
        //    var names = new NameConvention.CamelCaseResponseNameConvention();
        //    var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":\"just_an_error_message\"}}", names.Draw, names.Error);

        //    // Act
        //    var response = DataTablesResponse.Create(request, "just_an_error_message");

        //    // Assert
        //    Assert.Equal(expectedJson, response.ToString());
        //}
        /// <summary>
        /// Validates error response serialization.
        /// </summary>
        //[Fact]
        //public void ErrorResponseSerializationWithAditionalParameters()
        //{
        //    // Arrange
        //    var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
        //    var names = new NameConvention.CamelCaseResponseNameConvention();
        //    var aditionalParameters = TestHelper.MockAdditionalParameters();
        //    var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":\"just_an_error_message\",\"firstParameter\":\"firstValue\",\"secondParameter\":7}}", names.Draw, names.Error);

        //    // Act
        //    var response = DataTablesResponse.Create(request, "just_an_error_message", aditionalParameters);

        //    // Assert
        //    Assert.Equal(expectedJson, response.ToString());
        //}
        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        [Fact]
        public void ResponseSerializationWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var expectedJson = string.Format("{{\"{0}\":3,\"{1}\":2000,\"{2}\":1000,\"{3}\":{4}}}",
                names.Draw,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                JsonConvert.SerializeObject(data, new JsonSerializerSettings {  ContractResolver = new CamelCasePropertyNamesContractResolver() }));

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data);

            // Assert
            Assert.Equal(expectedJson, response.ToString());
        }
        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        //[Fact]
        //public void ResponseSerializationWithAditionalParameters()
        //{
        //    // Arrange
        //    var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
        //    var data = TestHelper.MockData();
        //    var names = new NameConvention.CamelCaseResponseNameConvention();
        //    var aditionalParameters = TestHelper.MockAdditionalParameters();
        //    var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":2000,\"{2}\":1000,\"{3}\":{4},\"firstParameter\":\"firstValue\",\"secondParameter\":7}}",
        //        names.Draw,
        //        names.TotalRecords,
        //        names.TotalRecordsFiltered,
        //        names.Data,
        //        Newtonsoft.Json.JsonConvert.SerializeObject(data),
        //        Newtonsoft.Json.JsonConvert.SerializeObject(aditionalParameters));

        //    // Act
        //    var response = DataTablesResponse.Create(request, 2000, 1000, data, aditionalParameters);

        //    // Assert
        //    Assert.Equal(expectedJson, response.ToString());
        //}
    }
}