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

        public async Task<List<GeocodingResultDTO>> Buscar(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<GeocodingResultDTO>();

            try
            {
                var url = $"api/Geocoding/Buscar?query={Uri.EscapeDataString(query)}";
                return await _httpClient.GetFromJsonAsync<List<GeocodingResultDTO>>(url) ?? new List<GeocodingResultDTO>();
            }
            catch (HttpRequestException)
            {
                return new List<GeocodingResultDTO>();
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
