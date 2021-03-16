using System;
using System.Net;

namespace frameworkless_web_application_kata
{
    public class Server
    {
        private UserService _userService;
        private HttpListener _listener;
        public bool IsListening { get; set; }
        public Server(UserService userService)
        {
            _userService = userService;
            _listener = new HttpListener();
        }

        public void Start()
        {
            _listener.Start();
            _listener.Prefixes.Add("http://*:8080/");
            IsListening = true;
            Console.WriteLine("Listening on port 8080...");
        }

        public void Listen()
        {
            while (IsListening)
            {
                var context = _listener.GetContext();
                
                var request = ProcessRequest(context);
                ProcessResponse(request, context);
            }
        }

        public Request ProcessRequest(HttpListenerContext context)
        {
            var r = context.Request;
            var requestCreator = new RequestCreator();
            return requestCreator.Create(r); //creates custom request object from context
            
        }

        public void ProcessResponse(Request request, HttpListenerContext context)
        {
            var requestRouter = new RequestRouter(request, _userService);
            Console.WriteLine($"{request.Method} {request.Url}");
            try
            {
                var response = requestRouter.Route(); //instantiates required controller and returns response
                var httpListenerResponse = context.Response;
                var responseProcessor = new ResponseProcessor(httpListenerResponse);
                responseProcessor.SendResponse(response);
            }
            catch (Exception)
            {
                var exceptionResponse = new Response(500, "internal server error");
                Console.WriteLine(exceptionResponse);
            }
        }
    }
}
