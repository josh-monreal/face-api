using FA.External;
using FA.External.APIs;
using FA.External.Core;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FA.UnitTests.FA.External.APIs
{
    [TestFixture]
    public class FaceAPITests
    {
        private FaceAPIs _api;
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
                .Returns(new HttpClient((_messageHandler.Object)));
        }

        [Test]
        public void Detect_WhenCalled_CreatePostRequest()
        {
            _content = new byte[] { };

            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                .Returns(new ByteArrayContent(_content));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Detect("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync", 
                    Times.Exactly(TimesCalled), 
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Detect_WhenCalled_RequestUriMustBeEqualToExpectedUri()
        {
            _uri =  new Uri(APISettings.UriBase + $"detect?returnFaceId=true");

            _content = new byte[] { };
            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                .Returns(new ByteArrayContent(_content));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Detect("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Detect_WhenCalled_RequestMethodShouldBePost()
        {
            _content = new byte[] { };
            _helper.Setup(hlpr => hlpr.CreateByteArrayContent("a", "b"))
                .Returns(new ByteArrayContent(_content));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Detect("a");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Identify_WhenCalled_CreatePostRequest()
        {
            _helper.Setup(hlpr => hlpr.CreateHttpContent(new List<string> { "a" }, "b"))
                .Returns(new StringContent("a"));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Identify(new List<string> { "a" }, "b");

            _messageHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(TimesCalled),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Identify_WhenCalled_RequestUriMustBeEqualToExpectedUri()
        {
            _uri = new Uri(APISettings.UriBase + "identify");

            _helper.Setup(hlpr => hlpr.CreateHttpContent(new List<string> { "a" }, "b"))
                .Returns(new StringContent("a"));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Identify(new List<string> { "a" }, "b");

            _messageHandler
             .Protected()
             .Verify(
                 "SendAsync",
                 Times.Exactly(TimesCalled),
                 ItExpr.Is<HttpRequestMessage>(req => req.RequestUri == _uri),
                 ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void Identify_WhenCalled_RequestMethodShouldBePost()
        {
            _helper.Setup(hlpr => hlpr.CreateHttpContent(new List<string> { "a" }, "b"))
                    .Returns(new StringContent("a"));

            _api = new FaceAPIs(_helper.Object);

            var result = _api.Identify(new List<string> { "a" }, "b");

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
