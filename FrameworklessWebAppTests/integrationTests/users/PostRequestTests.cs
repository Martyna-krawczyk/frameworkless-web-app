using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.users
{
    [Collection("TestServer collection")]
    public class PostRequestTests : IDisposable
    {
        private readonly HttpClient _httpClient;

        public PostRequestTests()
        {
            _httpClient = new HttpClient();
        }
        
        [Fact]
        public async Task PostRequestReturnsExpectedSuccessResponse()
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:8080/users")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("Clementine")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
                
            Assert.Contains("Clementine has been added", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task PostRequestReturnsReturnsErrorIfNameAlreadyInList()
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), "http://localhost:8080/users")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("peter")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
                
            Assert.Contains("that can't be done", response.Content.ReadAsStringAsync().Result); 
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}