using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace frameworkless_web_application_kata
{
    public class RequestRouter
    {
        private readonly UserService _userService;
        private readonly Request _request;

        public RequestRouter(Request request, UserService userService)
        {
            _request = request;
            _userService = userService;
        }
        
        public Response Route()
        {
            if (_request.Url == "/" || _request.Url == "")
            {
                var controller =  new RootController( _userService);
                return HandleRequest(controller);
            }
            var controllerName = _request.GetUrlSegments();
            if (controllerName[1] == "users")
            {
                return HandleRequest(new UserController(_request, _userService));
            }
            
            return new Response(404, "invalid path");
        }

        private Response HandleRequest(IController controller)
        {
            return _request.Method switch
            {
                "GET" => controller.HandleGetRequest(),
                "POST" => controller.HandlePostRequest(_request.Body),
                "DELETE" => controller.HandleDeleteRequest(),
                "PUT" => controller.HandlePutRequest(_request.Body),
                _ => new Response(404, "that resource can't be found")
            };
        }
    }
}