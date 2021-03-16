using System;
using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests.unitTests.root
{
    public class GetRequestTests
    {
        [Fact]
         public void GetRequestReturnsDefaultGreetingResponse()
           {
               var userRepository = new UserRepository();
               var userService = new UserService(userRepository);
               var controller = new RootController(userService);

               var time = DateTime.Now.ToString("%h:mm tt");
               var date = DateTime.Now.ToString("%d MMMM yyyy");
               var expected = $"Hello Martyna - the time on the server is {time} on {date}";

               var response = controller.HandleGetRequest();
               var actual = response.Body;
               
               Assert.Equal(expected, actual);
           }
         
         [Fact]
         public void GetRequestReturnsGreetingResponseWithTwoNamesInList()
           {
               var userRepository = new UserRepository();
               var userService = new UserService(userRepository);
               userService.AddUserToList("Emile");
               var controller = new RootController(userService);

               var time = DateTime.Now.ToString("%h:mm tt");
               var date = DateTime.Now.ToString("%d MMMM yyyy");
               var expected = $"Hello Martyna and Emile - the time on the server is {time} on {date}";

               var response = controller.HandleGetRequest();
               var actual = response.Body;
               
               Assert.Equal(expected, actual);
           }
         
         [Fact]
         public void GetRequestReturnsGreetingResponseWithFourNamesInList()
           {
               var userRepository = new UserRepository();
               var userService = new UserService(userRepository);
               userService.AddUserToList("emile");
               userService.AddUserToList("marcelo");
               userService.AddUserToList("david");
               var controller = new RootController(userService);

               var time = DateTime.Now.ToString("%h:mm tt");
               var date = DateTime.Now.ToString("%d MMMM yyyy");
               var expected = $"Hello Martyna, Emile, Marcelo and David - the time on the server is {time} on {date}";

               var response = controller.HandleGetRequest();
               var actual = response.Body;
               
               Assert.Equal(expected, actual);
           }
         
         [Fact]
         public void GetRequestReturns200StatusCodeIfSuccessful()
           {
               var userRepository = new UserRepository();
               var userService = new UserService(userRepository);
               var controller = new RootController(userService);

               var response = controller.HandleGetRequest();
               var actual = response.StatusCode;
               
               Assert.Equal(200, actual);
           }
         
         [Fact]
         public void PostRequestReturn405StatusCode()
           {
               var userRepository = new UserRepository();
               var userService = new UserService(userRepository);
               var request = new Request("/", "POST", "test");
               var controller = new RootController(userService);
         
               var response = controller.HandlePostRequest(request.Body);
               var actual = response.StatusCode;
               
               Assert.Equal(405, actual);
           }
    }
}
