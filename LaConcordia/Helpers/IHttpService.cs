using LaConcordia.Helpers;
using System.Threading.Tasks;

namespace LaConcordia.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<object>> Delete(string url);
    }
}
