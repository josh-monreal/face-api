using FA.Business;
using FA.Business.Core;
using FA.Business.DTOs;
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
    public class PersonLogicTests
    {
        private Mock<IPersonAPI> _personAPI;
        private Mock<IResponseHelper> _responseHelper;
        private PersonLogic _personLogic;

        [SetUp]
        public void SetUp()
        {
            _personAPI = new Mock<IPersonAPI>();
            _responseHelper = new Mock<IResponseHelper>();
        }

        [Test]
        public void Create_PersonGroupDoesNotExist_ResponseDataMustBeNull()
        {
            _personAPI.Setup(api => api.Create("a", "b"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound }));

            _responseHelper.Setup(rh => rh.CreateResponse<PersonDto>(
                    It.Is<HttpResponseMessage>(res => !res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = null });

            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            var result = _personLogic.Create("a", "b");

            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public void Create_PersonGroupExists_ResponseDataMustBeAnObject()
        {
            _personAPI.Setup(api => api.Create("a", "b"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK }));

            _responseHelper.Setup(rh => rh.CreateResponse<PersonDto>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = new PersonDto { PersonId = "123" }});

            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            var result = _personLogic.Create("a", "b");

            Assert.That(((PersonDto)result.Data).PersonId, Is.EqualTo("123"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_PersonGroupIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            Assert.That(() => _personLogic.Create(error, "a"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Create_PersonNameIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            Assert.That(() => _personLogic.Create("a", error), Throws.ArgumentNullException);
        }

        [Test]
        public void AddFace_PersonGroupDoesNotExist_ResponseDataMustBeNull()
        {
            _personAPI.Setup(api => api.AddFace("a", "b", "c"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound }));

            _responseHelper.Setup(rh => rh.CreateResponse<PersistedFaceDto>(
                    It.Is<HttpResponseMessage>(res => !res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = null });

            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            var result = _personLogic.AddFace("a", "b", "c");

            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public void AddFace_PersonGroupExists_ResponseDataMustBeAnObject()
        {
            _personAPI.Setup(api => api.AddFace("a", "b", "c"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK }));

            _responseHelper.Setup(rh => rh.CreateResponse<PersistedFaceDto>(
                    It.Is<HttpResponseMessage>(res => res.IsSuccessStatusCode),
                    It.IsAny<string>()))
                .Returns(new Response { Data = new PersistedFaceDto { PersistedFaceId = "123" } });

            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            var result = _personLogic.AddFace("a", "b", "c");

            Assert.That(((PersistedFaceDto)result.Data).PersistedFaceId, Is.EqualTo("123"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddFace_PersonGroupIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            Assert.That(() => _personLogic.AddFace(error, "a", "a"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddFace_PersonIdIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            Assert.That(() => _personLogic.AddFace("a", error, "a"), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void AddFace_ImagePathIsNullOrEmpty_ThrowArgumentNullException(string error)
        {
            _personLogic = new PersonLogic(_personAPI.Object, _responseHelper.Object);

            Assert.That(() => _personLogic.AddFace("a", "a", error), Throws.ArgumentNullException);
        }
    }
}
