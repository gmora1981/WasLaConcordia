using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class GenerarPlanAyudaRepository : IGenerarPlanAyuda
    {
        private readonly HttpClient _httpClient;

        public GenerarPlanAyudaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BeneficiarioDTO>> GetBeneficiariosPorAfiliado(string ciAfiliado)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BeneficiarioDTO>>($"api/GenerarPlanAyuda/GetBeneficiariosPorAfiliado/{ciAfiliado}") ?? new List<BeneficiarioDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los beneficiarios.", ex);
            }
        }

        public async Task<bool> YaFueGenerado(string beneficiario)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/GenerarPlanAyuda/YaFueGenerado/{beneficiario}");
        }

        public async Task<GenerarPlanResultadoDTO> GenerarPlanAyuda(GenerarPlanAyudaRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/GenerarPlanAyuda/GenerarPlanAyuda", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadFromJsonAsync<GenerarPlanResultadoDTO>() ?? new GenerarPlanResultadoDTO();
        }
    }
}
