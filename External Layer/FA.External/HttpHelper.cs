using FA.External.Core;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FA.External
{
    public class HttpHelper : IHttpHelper
    {
        private readonly string _subscriptionKey;

        public HttpHelper()
        {
            _subscriptionKey = new APISettings().SubscriptionKey;
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);

            return client;
        }

        public HttpContent CreateHttpContent(object obj, string contentType)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return content;
        }

        public ByteArrayContent CreateByteArrayContent(string imagePath, string contentType)
        {
            FileStream fileStream = new FileStream(imagePath, 
                FileMode.Open, 
                FileAccess.Read);

            BinaryReader binaryReader = new BinaryReader(fileStream);
            var data = binaryReader.ReadBytes((int)fileStream.Length);
            ByteArrayContent content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return content;
        }
    }
}
