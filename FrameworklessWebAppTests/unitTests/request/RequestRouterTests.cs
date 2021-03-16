using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.request
{
    public class RequestRouterTests
    {
        [Fact]
        public void PostRequestToUsersEndpointReturnsResponseBody()
        {
            var request = new Request("/users", "POST", "bob");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var requestRouter = new RequestRouter(request, userService);

            var expected = "Bob has been added";
            var actual = requestRouter.Route().Body;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetRequestToUsersEndpointReturnsListOfUsers()
        {
            var request = new Request("/users", "GET", "");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.AddUserToList("peter");
            var requestRouter = new RequestRouter(request, userService);
            
            var expected = "Martyna, Peter";
            var actual = requestRouter.Route().Body;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("/coffee", "PUT", "james")]
        [InlineData("/test/pizza", "GET", "")]
        public void IncorrectPathReturns404(string path, string method, string body)
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request(path, method, body);
            var requestRouter = new RequestRouter(request, userService);
            
            var expected = "invalid path";
            var actual = requestRouter.Route();
            
            Assert.Equal(expected, actual.Body);
            Assert.Equal(404, actual.StatusCode);
        } 
        
        [Theory]
        [InlineData("/users", "DELETE", "bread")]
        public void RequestWithoutResourceReturnsError(string path, string method, string body)
        {
            var request = new Request(path, method, body);
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var requestRouter = new RequestRouter(request, userService);
            
            var expected = "uri resource empty";
            var actual = requestRouter.Route().Body;

            Assert.Contains(expected, actual);
        }
    }
}