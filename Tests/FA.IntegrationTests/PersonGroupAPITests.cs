using FA.External;
using FA.External.APIs;
using FA.IntegrationTests.Common;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace FA.IntegrationTests
{
    [SetUpFixture]
    public class IntegrationTestSetup
    {
        [OneTimeSetUp]
        public async Task SetUp()
        {
            var api = new PersonGroupAPIs(new HttpHelper());

            await api.Create(ParameterConstants.PersonGroupId);
        }

        [OneTimeTearDown]
        public async Task CleanUp()
        {
            var uri = $"{ APIConstants.UriBase }persongroups/{ ParameterConstants.PersonGroupId }";
            var client = new HttpHelper().GetHttpClient();

            await client.DeleteAsync(uri);
        }
    }

    [TestFixture]
    public class PersonGroupAPITests
    {
        private PersonGroupAPIs _api;

        [SetUp]
        public void SetUp()
        {
            _api = new PersonGroupAPIs(new HttpHelper());
        }

        [Test]
        public void Create_WhenCalled_ReturnHttpStatusCodeOfConflict()
        {
            var result = _api.Create(ParameterConstants.PersonGroupId)
                .Result;

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public void Create_WhenCalled_ErrorCodeMustHaveAValue()
        {
            var result = _api
                .Create(ParameterConstants.PersonGroupId)
                .Result;

            Assert.That(result.GetStringContent(), Contains.Substring("PersonGroupExists"));
        }

        [Test]
        public void Create_WhenCalled_ContentTypeShouldBeJson()
        {
            var result = _api.Create(ParameterConstants.PersonGroupId)
                .Result;

            Assert.That(result.GetContentType().MediaType, Contains.Substring("application/json"));
        }

        [Test]
        public void Train_WhenCalled_ReturnHttpStatusCodeOfAccepted()
        {
            var result = _api.Train(ParameterConstants.PersonGroupId)
                .Result;

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        }

        [Test]
        public void Train_WhenCalled_ContentShouldBeEmpty()
        {
            var result = _api.Train(ParameterConstants.PersonGroupId)
                .Result;

            Assert.That(result.GetStringContent(), Is.Empty);
        }
    }
}
