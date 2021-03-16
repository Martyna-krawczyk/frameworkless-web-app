using System;
using System.Linq;
using System.Net;
using frameworkless_web_application_kata;

namespace frameworkless_web_application_kata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            var server = new Server(userService);
            
            server.Start();
            server.Listen();
        }
    }
}
