using FA.Business.Models;
using FA.Business.Core;
using FA.External.Core;
using System;

namespace FA.Business
{
    public class PersonGroupLogic : IPersonGroupLogic
    {
        private readonly IPersonGroupAPI _personGroupAPI;
        private readonly IResponseHelper _responseHelper;

        public PersonGroupLogic(IPersonGroupAPI personGroupAPI,
            IResponseHelper responseHelper)
        {
            _personGroupAPI = personGroupAPI;
            _responseHelper = responseHelper;
        }

        public Response Create(string personGroupId)
        {
            if (string.IsNullOrWhiteSpace(personGroupId))
                throw new ArgumentNullException("personGroupId", "The person group ID that you entered is invalid");

            var result = _personGroupAPI.Create(personGroupId)
                .Result;

            return _responseHelper.CreateResponse<bool>
                (result, $"Person-group '{ personGroupId }' has been successfully created.");
        }

        public Response Train(string personGroupId)
        {
            if (string.IsNullOrWhiteSpace(personGroupId))
                throw new ArgumentNullException("personGroupId", "The person group ID that you entered is invalid");

            var result = _personGroupAPI.Train(personGroupId)
                .Result;

            return _responseHelper.CreateResponse<bool>
                (result, $"Person-group '{ personGroupId }' has been successfully trained.");
        }
    }
}
