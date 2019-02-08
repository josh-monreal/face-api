using FA.Business.Models;
using FA.Business.Core;
using FA.External.Core;
using System;
using FA.Business.DTOs;
using FA.Business.Validators;

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

        public Response Create(PersonGroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("dto", "The object that you provided cannot be null.");

            PersonGroupValidator validator = new PersonGroupValidator();
            var validationResults = validator.Validate(dto);

            if (validationResults.IsValid)
            {
                var result = _personGroupAPI.Create(dto.PersonGroupId)
                    .Result;

                return _responseHelper.CreateResponse<bool>(
                    result,
                    $"Person-group '{ dto.PersonGroupId }' has been successfully created.");
            }
            else
                throw new InvalidOperationException("The person group ID that you have provided is invalid.");
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
