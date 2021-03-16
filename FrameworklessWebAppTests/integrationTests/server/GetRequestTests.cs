using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.server
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
        public async Task InvalidPathReturns404NotFound()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/fake-path");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode );
        }
        
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}