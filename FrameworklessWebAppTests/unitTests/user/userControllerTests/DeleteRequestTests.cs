using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user
{
    public class DeleteRequestTests
    {
        [Fact]
        public void DeleteRemovesUserFromList()
        {
            var userRepository = new UserRepository();
            userRepository.Add("john");
            var userService = new UserService(userRepository);
            var request = new Request("/users/john", "DELETE", "");
            var controller = new UserController(request, userService);
            
            var actual = controller.HandleDeleteRequest();
            
            Assert.Equal(200, actual.StatusCode);
            Assert.DoesNotContain("john", userRepository.Users);
        } 
        
        [Fact]
        public void DeleteRequestReturnsSuccessMessage()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.UserRepository.Add("emile");
            var request = new Request("/users/emile", "POST", "");
            var controller = new UserController(request, userService);
            
            var expected = "Emile has been removed from the list";
            var actual = controller.HandleDeleteRequest();
            
            Assert.Equal(expected, actual.Body);
            Assert.Equal(200, actual.StatusCode);
        } 
        
        [Fact]
        public void DeleteReturnsUnsuccessfulIfUserNotFound()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/users/john", "POST", "");
            var controller = new UserController(request, userService);
            
            var expected = "sorry that name doesn't exist";
            var actual = controller.HandleDeleteRequest();
            
            Assert.Equal(expected, actual.Body);
            Assert.Equal(404, actual.StatusCode);
        } 
        
        [Fact]
        public void DeleteRequestReturnsExpectedMessageWhenAttemptingToDeleteMartyna()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var request = new Request("/users/martyna", "DELETE", "");
            var controller = new UserController(request, userService);
            
            var expected = "sorry you cannot change the master user";
            var actual = controller.HandleDeleteRequest();
            
            Assert.Equal(expected, actual.Body);
        }
    }
}