using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.Core
{
    public interface IPersonAPI
    {
        Task<HttpResponseMessage> AddFace(string personGroupId,
            string personId,
            string imagePath);

        Task<HttpResponseMessage> Create(string personGroupId, string personName);
    }
}
