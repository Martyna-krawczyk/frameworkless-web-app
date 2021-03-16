using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user
{
    public class PutRequestTests
    {
        [Fact]
        public void PutRequestUpdatesUser()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.AddUserToList("john");
            var request = new Request("/users/john", "PUT", "james");
            var controller = new UserController(request, userService);
            
            var expected = "John has been updated to James";
            var actual = controller.HandlePutRequest(request.Body);
            
            Assert.Equal(expected, actual.Body);
            Assert.Contains("James", userRepository.Users);
            Assert.DoesNotContain("John", userRepository.Users);
        } 
        
        [Fact]
        public void PutRequestReturnsUserCreatedMessageIfUriResourceDoesntExist()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/users/john", "PUT", "james");
            var controller = new UserController(request, userService);
            
            var expected = "John has been added";
            var actual = controller.HandlePutRequest(request.Body);
            
            Assert.Equal(expected, actual.Body);
        }
        
        [Fact]
        public void PutRequestReturnsUnsuccessfulIfUpdatedNameAlreadyInList()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userRepository.Add("james");
            var request = new Request("/users/james", "PUT", "james");
            var controller = new UserController(request, userService);
            
            var expected = "sorry that name already exists";
            var actual = controller.HandlePutRequest(request.Body);
            
            Assert.Equal(expected, actual.Body);
        }
        
        [Fact]
        public void PutReturns400IfNoResourceUri()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/users/", "PUT", "");
            var controller = new UserController(request, userService);

            var actual = controller.HandlePutRequest(request.Body);
            
            Assert.Contains("no resource", actual.Body);
            Assert.Equal(400, actual.StatusCode);
        }
        
        [Fact]
        public void PutRequestReturnsExpectedMessageWhenAttemptingToModifyMartyna()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/users/martyna", "PUT", "monkey");
            var controller = new UserController(request, userService);
            
            var expected = "sorry you cannot change the master user";
            var actual = controller.HandlePutRequest(request.Body);
            
            Assert.Equal(expected, actual.Body);
        }
    }
}