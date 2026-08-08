using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGeocoding
    {
        Task<List<GeocodingResultDTO>> Buscar(string query);

        // Geocodificacion inversa: coordenadas -> texto de direccion legible.
        Task<string?> Reverse(decimal lat, decimal lon);
    }
}
