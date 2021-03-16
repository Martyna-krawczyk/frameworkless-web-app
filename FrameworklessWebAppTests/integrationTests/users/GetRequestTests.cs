using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.users
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
        public async Task GetRequestReturnsUsersWithExpectedSuccessResponse()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/users");
            
            Assert.Contains("Martyna, Sunshine, Peter", response.Content.ReadAsStringAsync().Result );
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task GetRequestReturnsUserSuccessfully()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/users/martyna");
            
            Assert.Equal("Martyna", response.Content.ReadAsStringAsync().Result );
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRequestReturns404IfUserNotFound()
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/users/unknownuser");
            
            Assert.Contains("not found", response.Content.ReadAsStringAsync().Result );
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}