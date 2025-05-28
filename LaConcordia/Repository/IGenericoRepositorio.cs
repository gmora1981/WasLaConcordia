using LaConcordia.Helpers;
using LaConcordia.Helpers;
using System.Threading.Tasks;

namespace LaConcordia.Repository
{
    public interface IGenericoRepositorio
    {
        Task<HttpResponseWrapper<T>> GetRegistrados<T>(string url);
    }
}
