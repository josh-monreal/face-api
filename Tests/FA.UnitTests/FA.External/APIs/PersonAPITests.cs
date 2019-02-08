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
    public class PersonAPITests
    {
        private PersonAPIs _api;
        private Mock<IHttpHelper> _helper;
        private Mock<HttpMessageHandler> _messageHandler;

        private byte[] _content;
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
        public void AddFace_WhenCalled_CreatePostRequest()
        {
            _content = new byte[] { };
            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                 .Returns(new ByteArrayContent(_content));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.AddFace("a", "b", "c");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void AddFace_WhenCalled_RequestUriMustBeEqualToExpectedUri()
        {
            _uri = new Uri(APISettings.UriBase + "persongroups/a/persons/b/persistedFaces");

            _content = new byte[] { };
            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                .Returns(new ByteArrayContent(_content));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.AddFace("a", "b", "c");

            _messageHandler
               .Protected()
               .Verify(
                   "SendAsync",
                   Times.Exactly(TimesCalled),
                   ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                   ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void AddFace_WhenCalled_RequestMethodShouldBePost()
        {
            _content = new byte[] { };
            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                .Returns(new ByteArrayContent(_content));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.AddFace("a", "b", "c");

            _messageHandler
               .Protected()
               .Verify(
                   "SendAsync",
                   Times.Exactly(TimesCalled),
                   ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                   ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Create_WhenCalled_CreatePostRequest()
        {
            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                .Returns(new StringContent("a"));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.Create("a", "b");

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
            _uri = new Uri(APISettings.UriBase + "persongroups/a/persons");

            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                .Returns(new StringContent("a"));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.Create("a", "b");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Create_WhenCalled_RequestMethodShouldBePost()
        {
            var item = new { Id = 1, Name = "a" };
            _helper.Setup(hlpr => hlpr.CreateHttpContent(item, "a"))
                .Returns(new StringContent("a"));

            _api = new PersonAPIs(_helper.Object);

            var result = _api.Create("a", "b");

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
