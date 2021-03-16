using System.Collections.Generic;
using System.Linq;
using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUserUpdatesUserList()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.AddUserToList("emile");

            Assert.Contains(userRepository.GetAll(), s => s == "Emile");
        }

        [Fact]
        public void GetUserMethodReturnsCapitalisedNames()
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.AddUserToList("john");
            userService.AddUserToList("peter");
            userService.AddUserToList("kai");

            var actual = userService.GetUserList();

            Assert.Equal(new List<string> {"Martyna", "John", "Peter", "Kai"}, actual);
        }
        
        [Fact]
        public void NameExistsMethodFiltersUsersRegardlessOfCase()
        {
            var userRepository = new UserRepository();
            userRepository.Users.Add("Kai"); 
            userRepository.Users.Add("Peter"); 
            userRepository.Users.Add("Mason");
            var userService = new UserService(userRepository);

            var actual = userService.NameExists("peter");
            
            Assert.True(actual);
        }
        
        [Fact]
        public void FindMatchReturnsUserIndexInList()
        {
            var userRepository = new UserRepository();
            userRepository.Users.Add("Peter");
            userRepository.Users.Add("Sarah");
            var userService = new UserService(userRepository);

            var actual = userService.FindMatch("peter");
            
            Assert.Equal(1, actual);
        }
    }
}