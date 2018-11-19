using FA.External.Core;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.APIs
{
    public class PersonGroupAPIs : IPersonGroupAPI
    {
        private readonly string _uriBase;
        private readonly HttpHelper _httpHelper;

        public PersonGroupAPIs()
        {
            _uriBase = APISettings.URI_BASE;
            _httpHelper = new HttpHelper(APISettings.SUBSCRIPTION_KEY);
        }

        public async Task<HttpResponseMessage> Create(string personGroupId)
        {
            var personGroup = new
            {
                Name = personGroupId,
                UserData = "Sample Data"
            };
            var content = _httpHelper.CreateHttpContent(personGroup, "application/json");

            string uri = _uriBase + $"persongroups/{ personGroupId }";

            var client = _httpHelper.GetHttpClient();

            return await client.PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> Train(string personGroupId)
        {
            string uri = _uriBase + $"persongroups/{ personGroupId }/train";

            var client = _httpHelper.GetHttpClient();

            return await client.PostAsync(uri, null);
        }
    }
}
