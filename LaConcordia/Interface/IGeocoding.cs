using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGeocoding
    {
        Task<List<GeocodingResultDTO>> Buscar(string query);
    }
}
