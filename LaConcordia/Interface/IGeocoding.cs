using LaConcordia.DTO;

namespace LaConcordia.Interface
{
    public interface IGeocoding
    {
        // Predicciones tipo autocompletar mientras el usuario escribe (Google Places Autocomplete).
        Task<List<PlacePredictionDTO>> BuscarPredicciones(string query);

        // Resuelve una prediccion ya seleccionada a sus coordenadas (Google Place Details).
        Task<GeocodingResultDTO?> ObtenerCoordenadasPorPlaceId(string placeId);

        // Geocodificacion inversa: coordenadas -> texto de direccion legible.
        Task<string?> Reverse(decimal lat, decimal lon);
    }
}
