using frameworkless_web_application_kata;
using Xunit;

namespace FrameworklessWebAppTests
{
    public class RequestCreatorTests
    {
        [Fact]
        public void BodyDecodedForUrlEncodedPostRequest()
        {
            var actual = RequestCreator.DecodeRawBody("Name:=Sina");

            var expected = "sina";
               
            Assert.Equal(expected, actual);
        }
    }
}