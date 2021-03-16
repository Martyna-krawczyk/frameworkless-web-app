using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.root
{
    public class PutRequestTests
    {
        [Fact]
        public void PutRequestReturns405StatusCode()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/", "PUT", "test");
            var controller = new RootController(userService);
      
            var response = controller.HandlePostRequest(request.Body);
            var actual = response.StatusCode;
            
            Assert.Equal(405, actual);
        }
    }
}