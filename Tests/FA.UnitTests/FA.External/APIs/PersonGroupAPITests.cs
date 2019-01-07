using FA.External;
using FA.External.APIs;
using FA.External.Core;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FA.UnitTests.FA.External.APIs
{
    [TestFixture]
    public class PersonGroupAPITests
    {
        private PersonGroupAPIs _api;
        private Mock<IHttpHelper> _helper;
        private Mock<HttpMessageHandler> _messageHandler;

        private Uri _uri;

        private const int TimesCalled = 1;

        [SetUp]
        public void SetUp()
        {
            _messageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _messageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage())
                .Verifiable();

            _helper = new Mock<IHttpHelper>();
            _helper.Setup(hlpr => hlpr.GetHttpClient())
                .Returns(new HttpClient(_messageHandler.Object));
        }

        [Test]
        public void Create_WhenCalled_CreatePutRequest()
        {
            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                 .Returns(new StringContent("a"));

            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Create("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Create_WhenCalled_RequestUriMustBeEqualToExpectedUri()
        {
            _uri = new Uri(APIConstants.UriBase + "persongroups/a");

            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                 .Returns(new StringContent("a"));

            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Create("a");

            _messageHandler
               .Protected()
               .Verify(
                   "SendAsync",
                   Times.Exactly(TimesCalled),
                   ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                   ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Create_WhenCalled_RequestMethodShouldBePut()
        {
            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                .Returns(new StringContent("a"));

            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Create("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Train_WhenCalled_CreatePostRequest()
        {
            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Train("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Train_WhenCalled_RequestUriMustBeEqualToExpectedUri()
        {
            _uri = new Uri(APIConstants.UriBase + "persongroups/a/train");
            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Train("a");

            _messageHandler
               .Protected()
               .Verify(
                   "SendAsync",
                   Times.Exactly(TimesCalled),
                   ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                   ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Train_WhenCalled_RequestMethodShouldBePost()
        {
            _api = new PersonGroupAPIs(_helper.Object);

            var result = _api.Train("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}
