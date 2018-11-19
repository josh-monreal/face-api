using FA.Business.Models;
using System.Net.Http;

namespace FA.Business.Core
{
    public interface IResponseHelper
    {
        Response CreateResponse<T>(HttpResponseMessage result, string successMessage = null);
        string JsonPrettyPrint(HttpResponseMessage result, string successMessage = null);
    }
}
