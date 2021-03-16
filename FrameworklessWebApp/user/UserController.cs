using System;
using System.Linq;

namespace frameworkless_web_application_kata
{
    public class UserController : IController
    {
        private readonly Request _request;
        private readonly UserService _userService;

        public UserController(Request request, UserService userService)
        {
            _request = request;
            _userService = userService;
        }

        public Response HandleGetRequest()
        {
            if (_request.GetUrlSegments().Length == 2)
            {
                var userNameList = Formatter.PrintNames(_userService.GetUserList());
                return new Response(200, $"{userNameList}"); 
            }

            if (_request.GetUrlSegments().Length == 3)
            {
                var requestedResource = _request.GetUrlSegments()[2].ToLower();
                if (_userService.NameExists(requestedResource)) 
                {
                    return new Response(200, Formatter.Capitalise(requestedResource));
                } 
            }
            
            return new Response(404, "resource not found");
        }

        public Response HandlePostRequest(string name)
        {
            if (name == string.Empty)
            {
                return new Response(400,"no body found");
            }
            return _userService.NameExists(name) ? new Response(403,"sorry that can't be done") : CreateUser(name);
        }

        private Response CreateUser(string name)
        {
            _userService.AddUserToList(name);
            return new Response(200, $"{Formatter.Capitalise(name)} has been added");
        }

        public Response HandleDeleteRequest()
        {
            var pathSegments = _request.GetUrlSegments();
            if (pathSegments.Length > 2)
            {
                var resource = pathSegments[2].ToLower();

                if (!_userService.NameExists(resource))
                {
                    return new Response(404, "sorry that name doesn't exist");
                }
                
                if (resource == "martyna")
                {
                    return new Response(403, "sorry you cannot change the master user");
                }
                
                _userService.DeleteUser(resource);
                return new Response(200, $"{Formatter.Capitalise(resource)} has been removed from the list");
            }
            return new Response(400, "uri resource empty. Who would you like to delete?");
        }

        public Response HandlePutRequest(string name)
        {
            var pathSegments = _request.GetUrlSegments();

            if (pathSegments.Length <= 2)
            {
                return new Response(400, "no resource provided");
            }
            var resource = pathSegments[2].ToLower();
            
            if (pathSegments[2] == "")
            {
                return new Response(400, "no resource provided");
            }
            
            if (!_userService.NameExists(resource))
            {
                return CreateUser(resource);
            }
        
            if (_userService.NameExists(name))
            {
                return new Response(403, "sorry that name already exists");
            }

            if (resource == "martyna")
            {
                return new Response(403, "sorry you cannot change the master user");
            }
        
            _userService.UpdateUser(name, resource);
            return new Response(200, $"{Formatter.Capitalise(resource)} has been updated to {Formatter.Capitalise(name)}");
        }
    }
}