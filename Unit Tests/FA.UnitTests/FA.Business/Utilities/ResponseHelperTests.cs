using FA.Business.Models;
using FA.Business.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;

namespace FA.UnitTests.FA.Business.Utilities
{
    public class SampleClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class ResponseHelperTests
    {
        private ResponseHelper _responseHelper;
        private HttpResponseMessage _response;
        private ErrorResponse _errorResponse;
        private SampleClass _class;

        [SetUp]
        public void SetUp()
        {
            _responseHelper = new ResponseHelper();
        }

        [Test]
        [TestCase(HttpStatusCode.Accepted)]
        [TestCase(HttpStatusCode.OK)]
        public void CreateResponse_IsSuccessStatusCodeIsTrueAndTypeIsBoolean_ReturnResponseDataAsTrue(HttpStatusCode httpStatusCode)
        {
            _response = new HttpResponseMessage { StatusCode = httpStatusCode };

            var result = _responseHelper.CreateResponse<bool>(_response, "a");

            Assert.That(result.Data, Is.True);
        }

        [Test]
        [TestCase(HttpStatusCode.Accepted)]
        [TestCase(HttpStatusCode.OK)]
        public void CreateResponse_IsSuccessStatusCodeIsTrueAndTypeIsObject_ReturnResponseDataAsObject(HttpStatusCode httpStatusCode)
        {
            _class = new SampleClass { Id = 1, Name = "Name" };
            var json = JsonConvert.SerializeObject(_class);
            _response = new HttpResponseMessage { StatusCode = httpStatusCode, Content = new StringContent(json, Encoding.UTF8, "application/json") };

            var result = _responseHelper.CreateResponse<SampleClass>(_response, "a");

            Assert.That(result.Data, Is.TypeOf<SampleClass>());
        }

        [Test]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.Conflict)]
        public void CreateResponse_IsSuccessStatusCodeIsFalseAndTypeIsBoolean_ReturnResponseDataAsFalse(HttpStatusCode httpStatusCode)
        {
            _errorResponse = new ErrorResponse { Error = new ResponseMessage { Code = "a", Message = "a" } };
            var json = JsonConvert.SerializeObject(_errorResponse);
            _response = new HttpResponseMessage { StatusCode = httpStatusCode, Content = new StringContent(json, Encoding.UTF8, "application/json") };

            var result = _responseHelper.CreateResponse<bool>(_response, "a");

            Assert.That(result.Data, Is.False);
        }

        [Test]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.Conflict)]
        public void CreateResponse_IsSuccessStatusCodeIsFalseAndTypeIsObject_ReturnResponseDataAsNull(HttpStatusCode httpStatusCode)
        {
            _errorResponse = new ErrorResponse { Error = new ResponseMessage { Code = "a", Message = "a" } };
            var json = JsonConvert.SerializeObject(_errorResponse);
            _response = new HttpResponseMessage { StatusCode = httpStatusCode, Content = new StringContent(json, Encoding.UTF8, "application/json") };

            var result = _responseHelper.CreateResponse<SampleClass>(_response, "a");

            Assert.That(result.Data, Is.Null);
        }
  
        [Test]
        public void JsonPrettyPrint_HttpContentIsNull_ReturnEmptyString()
        {
            _response = new HttpResponseMessage { Content = new StringContent("") };

            var result = _responseHelper.JsonPrettyPrint(_response);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void JsonPrettyPrint_HttpContentIsNotNull_ReturnPrettyString()
        {
            _response = new HttpResponseMessage { Content = new StringContent("[{\"a\": \"a\"}]") };

            var result = _responseHelper.JsonPrettyPrint(_response);

            Assert.That(result, Is.EqualTo("[\r\n   {\r\n      \"a\": \"a\"\r\n   }\r\n]"));
        }
    }
}
