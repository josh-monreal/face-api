using System.Net.Http;
using System.Net.Http.Headers;

namespace FA.IntegrationTests.Common
{
    public static class HttpResponseMessageExtensions
    {
        public static string GetStringContent(this HttpResponseMessage response)
        {
            return response
                .Content
                .ReadAsStringAsync()
                .Result;
        }

        public static MediaTypeHeaderValue GetContentType(this HttpResponseMessage response)
        {
            return response
                .Content
                .Headers
                .ContentType;
        }
    }
}
