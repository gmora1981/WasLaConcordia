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
    }
}
