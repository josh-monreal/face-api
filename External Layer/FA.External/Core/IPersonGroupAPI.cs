using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.Core
{
    public interface IPersonGroupAPI
    {
        Task<HttpResponseMessage> Create(string personGroupId);
        Task<HttpResponseMessage> Train(string personGroupId);
    }
}
