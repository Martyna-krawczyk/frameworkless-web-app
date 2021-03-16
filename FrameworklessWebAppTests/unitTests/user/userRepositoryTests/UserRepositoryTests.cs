using System.Linq;
using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.user.userRepositoryTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void DefaultNameInListWhenRepositoryIsInitialised()
        {
            var userRepository = new UserRepository();

            var actual = userRepository.Users.Any(n => n == "Martyna");
            
            Assert.True(actual);
        }
    }
}