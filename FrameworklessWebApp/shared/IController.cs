namespace frameworkless_web_application_kata
{
    public interface IController
    {
        public Response HandleGetRequest();

        public Response HandlePostRequest(string name);
        
        public Response HandleDeleteRequest();
        
        public Response HandlePutRequest(string name);
    }
}