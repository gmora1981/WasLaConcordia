using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class GenerarMultaRepository : IGenerarMulta
    {
        private readonly HttpClient _httpClient;

        public GenerarMultaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GenerarMultaDTO>> GetMultasPorSocio(string cidentidad)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<GenerarMultaDTO>>($"api/GenerarMulta/GetMultasPorSocio/{cidentidad}") ?? new List<GenerarMultaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las multas del socio.", ex);
            }
        }

        public async Task<GenerarMultaDTO> InsertGenerarMulta(GenerarMultaDTO nueva)
        {
            var response = await _httpClient.PostAsJsonAsync("api/GenerarMulta/InsertGenerarMulta", nueva);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al registrar la multa: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<GenerarMultaDTO>() ?? nueva;
        }
    }
}
