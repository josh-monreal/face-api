using FA.Business;
using FA.Business.Core;
using FA.Business.DTOs.DetectedFace;
using FA.Business.Models;
using FA.External.Core;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA.UnitTests.FA.Business
{
    [TestFixture]
    public class FaceLogicTests
    {
        private Mock<IFaceAPI> _faceAPI;
        private Mock<IResponseHelper> _responseHelper;
        private FaceLogic _faceLogic;

        [SetUp]
        public void SetUp()
        {
            _faceAPI = new Mock<IFaceAPI>();
            _responseHelper = new Mock<IResponseHelper>();
            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);
        }

        [Test]
        public void DetectFace_InvalidImageFormat_ResponseDataMustBeNull()
        {
            _faceAPI.Setup(api => api.Detect("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest }));

            _responseHelper.Setup(rh => rh.CreateResponse<DetectedFaceDto[]>(
                    It.Is<HttpResponseMessage>(res => !res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = null });

            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            var result = _faceLogic.DetectFace("a");

            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public void DetectFace_ValidImageFormat_ResponseDataMustBeAnObject()
        {
            _faceAPI.Setup(api => api.Detect("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK }));

            _responseHelper.Setup(rh => rh.CreateResponse<DetectedFaceDto[]>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = new DetectedFaceDto[] { new DetectedFaceDto { FaceId = "abc" }}});

            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            var result = _faceLogic.DetectFace("a");

            Assert.That(((DetectedFaceDto[])result.Data)[0].FaceId, Is.EqualTo("abc"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void DetectFace_ImagePathIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            Assert.That(() => _faceLogic.DetectFace(error), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.BadRequest)]
        public void IdentifyFace_WhenCalled_ReturnAString(HttpStatusCode httpStatusCode)
        {
            _faceAPI.Setup(api => api.Identify(new List<string> { "a" }, "a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = httpStatusCode, Content = new StringContent("") }));

            _responseHelper.Setup(rh => rh.JsonPrettyPrint(
                    It.IsAny<HttpResponseMessage>(),
                    It.IsAny<string>()))
                .Returns(string.Empty);

            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            var result = _faceLogic.IdentifyFace(new List<string> { "a" }, "a");

            Assert.That(result, Is.TypeOf<string>());
        }

        [Test]
        public void IdentifyFace_FaceIDIsNull_ThrowArgumentNullException()
        {
            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            Assert.That(() => _faceLogic.IdentifyFace(null, "a"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IdentifyFace_PersonGroupIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _faceLogic = new FaceLogic(_faceAPI.Object, _responseHelper.Object);

            Assert.That(() => _faceLogic.IdentifyFace(new List<string>(), error), Throws.ArgumentNullException);
        }
    }
}
