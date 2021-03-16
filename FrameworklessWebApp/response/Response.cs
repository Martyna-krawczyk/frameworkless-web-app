using System.IO;
using System.Net;

namespace frameworkless_web_application_kata
{
    public class Response
    {
        public Response(int statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public string Body { get; set; }
        public int StatusCode { get; set; }
    }
}