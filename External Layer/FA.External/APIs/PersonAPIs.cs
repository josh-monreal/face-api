using FA.External.Core;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.APIs
{
    public class PersonAPIs : IPersonAPI
    {
        private readonly string _uriBase;
        private readonly IHttpHelper _httpHelper;

        public PersonAPIs(IHttpHelper httpHelper)
        {
            _uriBase = APIConstants.UriBase;
            _httpHelper = httpHelper;
        }

        public async Task<HttpResponseMessage> AddFace(string personGroupId, 
            string personId,
            string imagePath)
        {
            var content = _httpHelper.CreateByteArrayContent(imagePath, "application/octet-stream");

            string uri = _uriBase + $"persongroups/{personGroupId}/persons/{personId}/persistedFaces";

            var client = _httpHelper.GetHttpClient();

            return await client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> Create(string personGroupId, string personName)
        {
            var person = new
            {
                Name = personName,
                UserData = "Sample Data"
            };
            var content = _httpHelper.CreateHttpContent(person, "application/json");

            string uri = _uriBase + $"persongroups/{ personGroupId }/persons";

            var client = _httpHelper.GetHttpClient();

            return await client.PostAsync(uri, content);
        }
    }
}
