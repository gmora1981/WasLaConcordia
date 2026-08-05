using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class GenerarPlanChoqueRepository : IGenerarPlanChoque
    {
        private readonly HttpClient _httpClient;

        public GenerarPlanChoqueRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> YaFueGenerado(string unidad)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/GenerarPlanChoque/YaFueGenerado/{unidad}");
        }

        public async Task<GenerarPlanResultadoDTO> GenerarPlanChoque(GenerarPlanChoqueRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/GenerarPlanChoque/GenerarPlanChoque", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadFromJsonAsync<GenerarPlanResultadoDTO>() ?? new GenerarPlanResultadoDTO();
        }
    }
}
