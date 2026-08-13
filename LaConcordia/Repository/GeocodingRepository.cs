using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class GeocodingRepository : IGeocoding
    {
        private readonly HttpClient _httpClient;

        public GeocodingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlacePredictionDTO>> BuscarPredicciones(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<PlacePredictionDTO>();

            try
            {
                var url = $"api/Geocoding/BuscarPredicciones?query={Uri.EscapeDataString(query)}";
                return await _httpClient.GetFromJsonAsync<List<PlacePredictionDTO>>(url) ?? new List<PlacePredictionDTO>();
            }
            catch (HttpRequestException)
            {
                return new List<PlacePredictionDTO>();
            }
        }

        public async Task<GeocodingResultDTO?> ObtenerCoordenadasPorPlaceId(string placeId)
        {
            if (string.IsNullOrWhiteSpace(placeId))
                return null;

            try
            {
                var url = $"api/Geocoding/ObtenerCoordenadasPorPlaceId?placeId={Uri.EscapeDataString(placeId)}";
                return await _httpClient.GetFromJsonAsync<GeocodingResultDTO>(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<string?> Reverse(decimal lat, decimal lon)
        {
            try
            {
                var url = $"api/Geocoding/Reverse?lat={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&lon={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
                return await _httpClient.GetFromJsonAsync<string?>(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
