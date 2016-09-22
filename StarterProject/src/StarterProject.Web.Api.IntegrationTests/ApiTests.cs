namespace ReadingAnalytics.Web.Api.UnitTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

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
        public async Task API_Values()
        {
            // Act
            var response = await client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
