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

        public async Task<List<string>> GetMonitorasDisponibles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/ControlUnidad/GetMonitorasDisponibles") ?? new List<string>();
        }

        public async Task<List<string>> GetUnidadesConMovimientos()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/ControlUnidad/GetUnidadesConMovimientos") ?? new List<string>();
        }

        public async Task<List<ControlUnidadMovimientoDTO>> GetMovimientosPorRango(DateTime desde, DateTime hasta, string? monitora, string? unidad)
        {
            var url = $"api/ControlUnidad/GetMovimientosPorRango?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(monitora))
                url += $"&monitora={Uri.EscapeDataString(monitora)}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";

            return await _httpClient.GetFromJsonAsync<List<ControlUnidadMovimientoDTO>>(url) ?? new List<ControlUnidadMovimientoDTO>();
        }

        public async Task<byte[]> ExportarReporteIngresoSalidaPdf(DateTime desde, DateTime hasta, string? monitora, string? unidad)
        {
            var url = $"api/ControlUnidad/ExportarReporteIngresoSalidaPdf?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(monitora))
                url += $"&monitora={Uri.EscapeDataString(monitora)}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar el reporte: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
