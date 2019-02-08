using FA.Business.DTOs;
using FA.Business.Models;

namespace FA.Business.Core
{
    public interface IPersonGroupLogic
    {
        Response Create(PersonGroupDto dto);
        Response Train(string personGroupId);
    }
}
