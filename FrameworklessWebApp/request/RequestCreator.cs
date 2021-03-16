using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Web;

namespace frameworkless_web_application_kata
{
    public class RequestCreator
    {
        public Request Create(HttpListenerRequest requestFromContext)
        {
            return new Request(requestFromContext.Url.AbsolutePath, requestFromContext.HttpMethod, GetBody(requestFromContext));
        }

        private static string GetBody(HttpListenerRequest requestFromContext)
        {
            var body = GetRawBody(requestFromContext);

            return body == "" ? "" : DecodeRawBody(body);
        }
        
        private static string GetRawBody(HttpListenerRequest requestFromContext)
        {
            var reader = new StreamReader(requestFromContext.InputStream, requestFromContext.ContentEncoding);
            var body = reader.ReadToEnd();
            return body.ToLower().Trim();
        }

        public static string DecodeRawBody(string body)
        {
            var splitString = body.Split("=");
            return splitString[1].ToLower().Trim();
        }
    }
}