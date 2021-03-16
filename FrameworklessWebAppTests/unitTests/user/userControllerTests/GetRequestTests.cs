using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user
{
    public class GetRequestTests
    {
        [Fact]
        public void GetRequestReturns200StatusCode()
        {
            var request = new Request("/users", "GET", "peter");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var controller = new UserController(request, userService);

            var actual = controller.HandleGetRequest();

            Assert.Equal(200, actual.StatusCode);
        }        
        
        [Fact]
        public void GetRequestToUserReturnsUser()
        {
            var request = new Request("/users/peter", "GET", "");
            var userRepository = new UserRepository();
            userRepository.Add("peter");
            var userService = new UserService(userRepository);
            var controller = new UserController(request, userService);

            var actual = controller.HandleGetRequest();

            Assert.Equal("Peter", actual.Body);
        }
    }
}