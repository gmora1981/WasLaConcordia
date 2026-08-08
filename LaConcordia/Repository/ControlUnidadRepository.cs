using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class ControlUnidadRepository : IControlUnidad
    {
        private readonly HttpClient _httpClient;

        public ControlUnidadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UnidadServicioDTO>> GetFichaPersonalPorServicio(string estadoServicio)
        {
            return await _httpClient.GetFromJsonAsync<List<UnidadServicioDTO>>($"api/ControlUnidad/GetFichaPersonalPorServicio/{estadoServicio}") ?? new List<UnidadServicioDTO>();
        }

        public async Task MoverUnidad(MoverUnidadRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ControlUnidad/MoverUnidad", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        //exportar PDF
        public async Task<byte[]> ExportarPdf(string? turno)
        {
            var url = "api/ControlUnidad/exportarPDF";
            if (!string.IsNullOrEmpty(turno))
                url += $"?turno={Uri.EscapeDataString(turno)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar PDF: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
