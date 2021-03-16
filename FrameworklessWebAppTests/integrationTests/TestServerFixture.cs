using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using frameworkless_web_application_kata;

namespace FrameworklessWebAppTests
{
    public class TestServerFixture : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IRepository _userRepository = new TestUserRepository();

        public TestServerFixture()
        {
            _httpClient = new HttpClient();
            var userService = new UserService(_userRepository);
            var server = new Server(userService);
            server.Start();
            var thread = new Thread(server.Listen);
            thread.Start();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}