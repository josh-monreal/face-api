using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FA.External.Core
{
    public interface IFaceAPI
    {
        Task<HttpResponseMessage> Detect(string imageFilePath);
        Task<HttpResponseMessage> Identify(List<string> faceIds, string personGroupId);
    }
}
