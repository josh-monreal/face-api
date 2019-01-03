using System.Net.Http;

namespace FA.External.Core
{
    public interface IHttpHelper
    {
        HttpClient GetHttpClient();
        HttpContent CreateHttpContent(object obj, string contentType);
        ByteArrayContent CreateByteArrayContent(string imagePath, string contentType);
    }
}
