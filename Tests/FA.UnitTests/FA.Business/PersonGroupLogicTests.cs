using FA.Business;
using FA.Business.Core;
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
    public class PersonGroupLogicTests
    {
        private Mock<IPersonGroupAPI> _personGroupAPI;
        private Mock<IResponseHelper> _responseHelper;
        private PersonGroupLogic _personGroupLogic;

        [SetUp]
        public void SetUp()
        {
            _personGroupAPI = new Mock<IPersonGroupAPI>();
            _responseHelper = new Mock<IResponseHelper>();
        }

        [Test]
        public void Create_PersonGroupDoesNotExist_ResponseDataMustBeTrue()
        {
            _personGroupAPI.Setup(api => api.Create("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK }));
            
            _responseHelper.Setup(rh => rh.CreateResponse<bool>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode == true), 
                    It.IsAny<string>()))
                .Returns(new Response { Data = true });

            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            var result = _personGroupLogic.Create("a");

            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        public void Create_PersonGroupExists_ResponseDataMustBeFalse()
        {
            _personGroupAPI.Setup(api => api.Create("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.Conflict }));

            _responseHelper.Setup(rh => rh.CreateResponse<bool>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode == false),
                    It.IsAny<string>()))
                .Returns(new Response { Data = false });

            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            var result = _personGroupLogic.Create("a");

            Assert.IsFalse((bool)result.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_PersonGroupIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            Assert.That(() => _personGroupLogic.Create(error), Throws.ArgumentNullException);
        }

        [Test]
        public void Train_PersonGroupDoesNotExist_ResponseDataMustBeFalse()
        {
            _personGroupAPI.Setup(api => api.Train("a"))
              .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound }));

            _responseHelper.Setup(rh => rh.CreateResponse<bool>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode == false),
                    It.IsAny<string>()))
                .Returns(new Response { Data = false });

            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            var result = _personGroupLogic.Train("a");

            Assert.IsFalse((bool)result.Data);
        }

        [Test]
        public void Train_PersonGroupExists_ResponseDataMustBeTrue()
        {
            _personGroupAPI.Setup(api => api.Train("a"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.Accepted }));

            _responseHelper.Setup(rh => rh.CreateResponse<bool>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode == true),
                    It.IsAny<string>()))
                .Returns(new Response { Data = true });

            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            var result = _personGroupLogic.Train("a");

            Assert.IsTrue((bool)result.Data);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Train_PersonGroupIsInvalid_ThrowArgumentNullException(string error)
        {
            _personGroupLogic = new PersonGroupLogic(_personGroupAPI.Object, _responseHelper.Object);

            Assert.That(() => _personGroupLogic.Train(error), Throws.ArgumentNullException);
        }
    }
}
