using FA.External.Core;
using FA.Business.Core;
using FA.Business.DTOs;
using FA.Business.Models;
using System;
using FA.Business.Validators;

namespace FA.Business
{
    public class PersonLogic : IPersonLogic
    {
        private readonly IPersonAPI _personAPI;
        private readonly IResponseHelper _responseHelper;

        public PersonLogic(IPersonAPI personAPI,
            IResponseHelper responseHelper)
        {
            _personAPI = personAPI;
            _responseHelper = responseHelper;
        }

        public Response Create(string personGroupId, PersonDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException("dto", "The object that you provided cannot be null.");

                if (string.IsNullOrWhiteSpace(personGroupId))
                    throw new ArgumentNullException("personGroupId", "The person group ID that you entered is invalid");

                var validationResults = new PersonValidator().Validate(dto);

                if (validationResults.IsValid)
                {
                    var result = _personAPI.Create(personGroupId, dto.Name)
                        .Result;

                    return _responseHelper.CreateResponse<PersonDto>(
                        result, $"'{ dto.Name }' has been successfully created in the '{ personGroupId }' person-group.");
                }
                else
                    throw new InvalidOperationException("The person name that you have provided is invalid.");
            }

            catch (AggregateException aex)
            {
                throw new Exception($"Error Message:  { aex.Message }");
            }
        }

        public Response AddFace(string personGroupId, 
            string personId,
            string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(personGroupId))
                    throw new ArgumentNullException("personGroupId", "The person group ID that you entered is invalid");

                if (string.IsNullOrWhiteSpace(personId))
                    throw new ArgumentNullException("personId", "The person ID that you entered is invalid");

                if (string.IsNullOrWhiteSpace(imagePath))
                    throw new ArgumentNullException("imagePath", "The file path of the image that you entered is invalid");

                var result = _personAPI.AddFace
                        (personGroupId,
                        personId,
                        imagePath)
                    .Result;

                return _responseHelper.CreateResponse<PersistedFaceDto>(
                    result, $"The face data of the image has been successfully added to person ID '{ personId }'.");
            }

            catch (AggregateException aex)
            {
                throw new Exception($"Error Message:  { aex.Message }");
            }
        }
    }
}
