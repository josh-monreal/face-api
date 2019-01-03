using FA.External.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.APIs
{
    public class FaceAPIs : IFaceAPI
    {
        private readonly string _uriBase;
        private readonly IHttpHelper _httpHelper;

        public FaceAPIs(IHttpHelper httpHelper)
        {
            _uriBase = APISettings.URI_BASE;
            _httpHelper = httpHelper;
        }

        public async Task<HttpResponseMessage> Detect(string imageFilePath)
        {
            string uri = _uriBase + $"detect?returnFaceId=true";

            var content = _httpHelper.CreateByteArrayContent(imageFilePath, "application/octet-stream");

            var client = _httpHelper.GetHttpClient();

            return await client.PostAsync(uri, content);;
        }

        public async Task<HttpResponseMessage> Identify(List<string> faceIds, string personGroupId)
        {
            var requestBody = new
            {
                PersonGroupID = personGroupId,
                FaceIds = faceIds
            };
            var content = _httpHelper.CreateHttpContent(requestBody, "application/json");

            string uri = _uriBase + "identify";

            var client = _httpHelper.GetHttpClient();

            return await client.PostAsync(uri, content);
        }
    }
}
