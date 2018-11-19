using FA.Business;
using FA.Business.Core;
using FA.Business.DTOs.DetectedFace;
using FA.Business.Models;
using FA.External.Core;
using Moq;
using NUnit.Framework;
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
        }

        [Test]
        public void DetectFace_InvalidImageFormat_ResponseDataMustBeNull()
        {
            _faceAPI.Setup(api => api.Detect("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound }));

            _responseHelper.Setup(rh => rh.CreateResponse<DetectedFaceDto>(
                    It.Is<HttpResponseMessage>(res => !res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = null });
        }
    }
}
