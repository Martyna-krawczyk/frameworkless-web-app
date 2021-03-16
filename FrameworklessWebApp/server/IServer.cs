using System.Net;

namespace frameworkless_web_application_kata
{
    public interface IServer
    {
        public Request ProcessRequest();
        public void ProcessResponse(Request request, HttpListenerContext context);

    }
}