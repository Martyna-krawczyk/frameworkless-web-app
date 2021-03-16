using System;
using System.Runtime.InteropServices;

namespace frameworkless_web_application_kata
{
    public class RootController : IController
    {
        private readonly UserService _users;

        public RootController(UserService users)
        {
            _users = users;
        }

        public Response HandleGetRequest()
        {
            return new Response(
                200, Formatter.PrintGreetingMessage(_users.GetUserList())
            ); 
        }

        public Response HandlePostRequest(string name)
        {
            return new Response(405, "This type of request is not allowed");
        }

        public Response HandleDeleteRequest()
        {
            return new Response(405, "This type of request is not allowed");
        }

        public Response HandlePutRequest(string name)
        {
            return new Response(405, "This type of request is not allowed");
        }
    }
}