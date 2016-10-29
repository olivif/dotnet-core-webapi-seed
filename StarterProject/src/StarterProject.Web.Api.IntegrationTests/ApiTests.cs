namespace StarterProject.Web.Api.UnitTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text;

    [TestClass]
    public class ApiTests
    {
        private static TestServer server;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            // Arrange
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [TestMethod]
        public async Task API_Values_GetEmptyListOfValues()
        {
            // Act
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var listOfValues = JsonConvert.DeserializeObject<List<Value>>(responseContent);

            Assert.AreEqual(0, listOfValues.Count);
        }

        [TestMethod]
        public async Task API_Values_PostValue()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data1" };
            var valueJson = JsonConvert.SerializeObject(value);

            var stringContent = new StringContent(valueJson, UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/values", stringContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task API_Values_PostValueAndReadBack()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data1" };
            var valueJson = JsonConvert.SerializeObject(value);

            var stringContent = new StringContent(valueJson, UnicodeEncoding.UTF8, "application/json");

            var postResponse = await client.PostAsync("/api/values", stringContent);

            // Act
            var getResponse = await client.GetAsync("/api/values");
            getResponse.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.OK);

            var responseContent = getResponse.Content.ReadAsStringAsync().Result;
            var listOfValues = JsonConvert.DeserializeObject<List<Value>>(responseContent);

            Assert.AreEqual(0, listOfValues.Count);
        }
    }
}
