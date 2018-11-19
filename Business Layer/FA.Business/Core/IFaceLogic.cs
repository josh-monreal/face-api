using FA.Business.Models;
using System.Collections.Generic;

namespace FA.Business.Core
{
    public interface IFaceLogic
    {
        Response DetectFace(string imagePath);
        string IdentifyFace(List<string> faceIds, string personGroupId);
    }
}
