using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class PagosRepository : IPagos
    {
        private readonly HttpClient _httpClient;

        public PagosRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeudaSocioDTO?> GetDeudaSocio(string cedula)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DeudaSocioDTO>($"api/Pagos/GetDeudaSocio/{cedula}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> ExisteComprobante(string banco, string numComprobante)
        {
            var url = $"api/Pagos/ExisteComprobante?banco={Uri.EscapeDataString(banco)}&numComprobante={Uri.EscapeDataString(numComprobante)}";
            return await _httpClient.GetFromJsonAsync<bool>(url);
        }

        public async Task PagarCuota(PagoCuotaRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pagos/PagarCuota", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        public async Task PagarUbm(PagoUbmRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pagos/PagarUbm", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        public async Task<List<DetallePagoMonitoriaDTO>> GetDetallePagosPorUnidad(string unidad, DateTime desde, DateTime hasta)
        {
            var url = $"api/Pagos/GetDetallePagosPorUnidad?unidad={Uri.EscapeDataString(unidad)}&desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            return await _httpClient.GetFromJsonAsync<List<DetallePagoMonitoriaDTO>>(url) ?? new List<DetallePagoMonitoriaDTO>();
        }

        public async Task<byte[]> ExportarReporteDetallePagosPdf(string unidad, DateTime desde, DateTime hasta)
        {
            var url = $"api/Pagos/ExportarReporteDetallePagosPdf?unidad={Uri.EscapeDataString(unidad)}&desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar el reporte: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<ResumenMonitoriaDTO> GetResumenMonitoria(DateTime desde, DateTime hasta)
        {
            var url = $"api/Pagos/GetResumenMonitoria?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            return await _httpClient.GetFromJsonAsync<ResumenMonitoriaDTO>(url) ?? new ResumenMonitoriaDTO();
        }
    }
}
