using FA.Business.Core;
using FA.Business.DTOs.DetectedFace;
using FA.Business.Models;
using FA.External.Core;
using System;
using System.Collections.Generic;

namespace FA.Business
{
    public class FaceLogic : IFaceLogic
    {
        private readonly IFaceAPI _faceAPI;
        private readonly IResponseHelper _responseHelper;

        public FaceLogic(IFaceAPI faceAPI,
            IResponseHelper responseHelper)
        {
            _faceAPI = faceAPI;
            _responseHelper = responseHelper;
        }

        public Response DetectFace(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath))
                    throw new ArgumentNullException("imagePath", "The image path that you entered is invalid");

                var result = _faceAPI.Detect(imagePath)
                    .Result;

                return _responseHelper.CreateResponse<DetectedFaceDto[]>(
                    result, "The face data of the image you uploaded has successfully been detected.");
            }

            catch (AggregateException aex)
            {
                throw new Exception($"Error Message:  { aex.Message }");
            }
        }

        public string IdentifyFace(List<string> faceIds, string personGroupId)
        {
            try
            {
                if (faceIds is null)
                    throw new ArgumentNullException("faceIds", "The face ID that you entered is invalid");

                if (string.IsNullOrWhiteSpace(personGroupId))
                    throw new ArgumentNullException("personGroupId", "The person group that you entered is invalid");

                var result = _faceAPI.Identify(faceIds, personGroupId)
                    .Result;

                return _responseHelper.JsonPrettyPrint(
                    result, "The faces that you provided have been successfully identified.");
            }

            catch (AggregateException aex)
            {
                throw new Exception($"Error Message:  { aex.Message }");
            }
        }
    }
}
