using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user
{
    public class PostRequestTests
    {
        [Fact]
        public void PostRequestReturnsExpectedSuccessMessage()
        {
            var request = new Request("/users", "POST", "bob");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var controller = new UserController(request, userService);

            var expected = "Bob has been added";
            var actual = controller.HandlePostRequest(request.Body);
               
            Assert.Equal(expected, actual.Body);
            Assert.Equal(200, actual.StatusCode);
        }
        
        [Fact]
        public void PostRequestReturnsExpectedMessageIfAttemptingToAddDuplicateUser()
        {
            var request = new Request("/users", "POST", "martyna");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var controller = new UserController(request, userService);

            var expected = "sorry that can't be done";
            var actual = controller.HandlePostRequest(request.Body);
            
            Assert.Equal(expected, actual.Body);
            Assert.Equal(403, actual.StatusCode);
        }

        [Fact]
        public void PostRequestReturns400StatusCodeIfBodyEmpty()
        {
            var request = new Request("/users", "POST", "");
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var controller = new UserController(request, userService);
            
            var actual = controller.HandlePostRequest(request.Body);

            Assert.Equal(400, actual.StatusCode);
        }
    }
}