using FA.External;
using FA.External.APIs;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace FA.IntegrationTests
{
    [TestFixture]
    public class PersonGroupAPITests
    {
        private PersonGroupAPIs _api;
        private HttpHelper _helper;
        private string _uri;
        private const string PERSON_GROUP_NAME = "integration-test";

        [SetUp]
        public void SetUp()
        {
            _helper = new HttpHelper();
            _api = new PersonGroupAPIs(_helper);
            _uri = $"{ APISettings.URI_BASE }persongroups/{ PERSON_GROUP_NAME }";
        }

        [Test]
        public void Create_WhenCalled_CreatePutRequest()
        {
            var personGroup = new
            {
                Name = PERSON_GROUP_NAME,
                UserData = "Sample Data"
            };
            var content = _helper.CreateHttpContent(personGroup, "application/json");
            var client = _helper.GetHttpClient();

            var result = client.PutAsync(_uri, content)
                .Result;

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TearDown]
        public async Task CleanUp()
        {
            var client = _helper.GetHttpClient();

            await client.DeleteAsync(_uri);
        }
    }
}
