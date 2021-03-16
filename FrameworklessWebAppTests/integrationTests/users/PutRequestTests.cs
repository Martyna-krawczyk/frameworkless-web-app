using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace FrameworklessWebAppTests.integrationTests.users
{
    [Collection("TestServer collection")]
    public class PutRequestTests
    {
        private readonly HttpClient _httpClient;

        public PutRequestTests()
        {
            _httpClient = new HttpClient();
        }
    
        [Fact]
        public async Task PutRequestReturnsResponseSuccessfullyWithNameUpdated()
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), "http://localhost:8080/users/malcolm")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("malcomius")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
        
            var response = await _httpClient.PutAsync(request.RequestUri, request.Content);
            
            Assert.Contains("Malcolm has been updated to Malcomius", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    
        [Fact]
        public async Task PutRequestReturnsResponseSuccessfullyWithNameCreated()
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), "http://localhost:8080/users/jumbaliah")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
        
            var response = await _httpClient.SendAsync(request);
            
            Assert.Contains("Jumbaliah has been added", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task PutRequestReturns400IfNoResource()
        {
            var request = new HttpRequestMessage(new HttpMethod("PUT"), "http://localhost:8080/users")
            {
                Content = new StringContent($"data={Uri.EscapeDataString("")}")
            };
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
        
            var response = await _httpClient.SendAsync(request);
            
            Assert.Contains("no resource", response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}