using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.root
{
    [Collection("TestServer collection")]
    public class GetRequestTests : IDisposable
    {
        private readonly HttpClient _httpClient;

        public GetRequestTests()
        {
            _httpClient = new HttpClient();
        }
        
        [Fact]
        public async Task GetRequestReturnsExpectedSuccessResponse()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080");
        
            Assert.Contains("Martyna", response.Content.ReadAsStringAsync().Result );
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}