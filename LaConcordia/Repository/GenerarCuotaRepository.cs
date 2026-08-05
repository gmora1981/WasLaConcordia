using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class GenerarCuotaRepository : IGenerarCuota
    {
        private readonly HttpClient _httpClient;

        public GenerarCuotaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PendienteCuotaDTO>> GetPendientesPorPeriodo(string periodo, string semana)
        {
            try
            {
                var url = $"api/GenerarCuota/GetPendientesPorPeriodo?periodo={Uri.EscapeDataString(periodo)}&semana={Uri.EscapeDataString(semana)}";
                return await _httpClient.GetFromJsonAsync<List<PendienteCuotaDTO>>(url) ?? new List<PendienteCuotaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los socios pendientes de cuota.", ex);
            }
        }

        public async Task<GenerarCuotaResultadoDTO> GenerarCuotaSemanal(string periodo, string semana)
        {
            var url = $"api/GenerarCuota/GenerarCuotaSemanal?periodo={Uri.EscapeDataString(periodo)}&semana={Uri.EscapeDataString(semana)}";
            var response = await _httpClient.PostAsync(url, null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al generar la cuota semanal: {errorContent}");
            }
            return await response.Content.ReadFromJsonAsync<GenerarCuotaResultadoDTO>() ?? new GenerarCuotaResultadoDTO();
        }
    }
}
