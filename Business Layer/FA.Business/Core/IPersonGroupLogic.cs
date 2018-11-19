using FA.Business.Models;

namespace FA.Business.Core
{
    public interface IPersonGroupLogic
    {
        Response Create(string personGroupId);
        Response Train(string personGroupId);
    }
}
