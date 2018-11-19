using FA.Business.Models;

namespace FA.Business.Core
{
    public interface IPersonLogic
    {
        Response Create(string personGroupId, string personName);

        Response AddFace(string personGroupId,
            string personId,
            string imagePath);
    }
}
