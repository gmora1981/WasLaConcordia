using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;
using System.Text.Json;

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

            var url = $"api/Geocoding/BuscarPredicciones?query={Uri.EscapeDataString(query)}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await LeerMensajeError(response));

            return await response.Content.ReadFromJsonAsync<List<PlacePredictionDTO>>() ?? new List<PlacePredictionDTO>();
        }

        public async Task<GeocodingResultDTO?> ObtenerCoordenadasPorPlaceId(string placeId)
        {
            if (string.IsNullOrWhiteSpace(placeId))
                return null;

            var url = $"api/Geocoding/ObtenerCoordenadasPorPlaceId?placeId={Uri.EscapeDataString(placeId)}";
            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                throw new Exception(await LeerMensajeError(response));

            return await response.Content.ReadFromJsonAsync<GeocodingResultDTO>();
        }

        public async Task<string?> Reverse(decimal lat, decimal lon)
        {
            var url = $"api/Geocoding/Reverse?lat={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&lon={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await LeerMensajeError(response));

            return await response.Content.ReadFromJsonAsync<string?>();
        }

        private static async Task<string> LeerMensajeError(HttpResponseMessage response)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("error", out var errEl))
                    return errEl.GetString() ?? "Error al consultar la ubicación.";
            }
            catch
            {
                // el cuerpo no era el JSON esperado; se usa el mensaje generico de abajo
            }

            return $"Error al consultar la ubicación ({(int)response.StatusCode}).";
        }
    }
}
