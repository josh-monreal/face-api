using FA.Business.DTOs;
using FA.Business.Models;

namespace FA.Business.Core
{
    public interface IPersonLogic
    {
        Response Create(string personGroupId, PersonDto dto);

        Response AddFace(string personGroupId,
            string personId,
            string imagePath);
    }
}
