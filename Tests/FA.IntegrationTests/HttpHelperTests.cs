using FA.External;
using NUnit.Framework;

namespace FA.IntegrationTests.FA.External
{
    [TestFixture]
    public class HttpHelperTests
    {
        private HttpHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _helper = new HttpHelper();
        }

        [Test]
        public void GetHttpClient_WhenCalled_ShouldReturnHttpClientWithAPIKey()
        {
            var result = _helper.GetHttpClient();

            Assert.That(result.DefaultRequestHeaders.Contains("Ocp-Apim-Subscription-Key"), Is.True);
        }

        [Test]
        public void CreateHttpContent_WhenCalled_ShouldReturnHttpContentWithItem()
        {
            var item = new
            {
                Id = 1,
                Name = "a"
            };

            var result = _helper.CreateHttpContent(item, "application/json");

            Assert.That(
                result.ReadAsStringAsync()
                    .Result, 
                Contains.Substring("Id"));
        }
    }
}
