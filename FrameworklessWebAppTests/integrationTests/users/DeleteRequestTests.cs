using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.users
{
    [Collection("TestServer collection")]
    public class DeleteRequestTests : IDisposable
    {
        private readonly HttpClient _httpClient;

        public DeleteRequestTests()
        {
            _httpClient = new HttpClient();
        }
        
        [Fact]
        public async Task DeleteRequestReturnsExpectedResponse()
        {
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), "http://localhost:8080/users/george")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
                
            Assert.Contains("George has been removed from the list", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task DeleteRequestReturns404IfUserNotFound()
        {
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), "http://localhost:8080/users/unknownuser")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
                
            Assert.Contains("sorry that name doesn't exist", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [Fact]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}