using System.Net;

namespace frameworkless_web_application_kata
{
    public class ResponseProcessor : IResponseProcessor
    {
        private readonly HttpListenerResponse _httpListenerResponse;

        public ResponseProcessor(HttpListenerResponse httpListenerResponse)
        {
            _httpListenerResponse = httpListenerResponse;
        }

    public void SendResponse(Response response)
        {
            _httpListenerResponse.StatusCode = response.StatusCode;

            var buffer = System.Text.Encoding.UTF8.GetBytes(response.Body);
            _httpListenerResponse.ContentLength64 = buffer.Length;
            _httpListenerResponse.OutputStream.Write(buffer); 
            _httpListenerResponse.OutputStream.Close();
            //todo - should the listener be stopped here? Graceful shutdown??
        }
    }
}