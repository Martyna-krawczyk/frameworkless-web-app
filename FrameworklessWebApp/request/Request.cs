using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Text;

namespace frameworkless_web_application_kata
{
    public class Request
    {
        public Request(string url, string method, string body)
        {
            Url = url;
            Method = method;
            Body = body;
        }
        
        public string Method { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        
        public string[] GetUrlSegments()
        {
            var segments = Url.Split("/");
            return segments;
        }
    }
}