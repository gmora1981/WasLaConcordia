using LaConcordia.Helpers;
using System.Threading.Tasks;

namespace LaConcordia.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
    }
}
