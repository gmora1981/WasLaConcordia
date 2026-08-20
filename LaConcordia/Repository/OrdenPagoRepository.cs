using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class OrdenPagoRepository : IOrdenPago
    {
        private readonly HttpClient _httpClient;

        public OrdenPagoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PedidoVoucherDTO>> GetPedidosPendientesVoucher(string? ruc = null)
        {
            var url = "api/OrdenPago/GetPedidosPendientesVoucher";
            if (!string.IsNullOrEmpty(ruc))
                url += $"?ruc={Uri.EscapeDataString(ruc)}";

            return await _httpClient.GetFromJsonAsync<List<PedidoVoucherDTO>>(url) ?? new List<PedidoVoucherDTO>();
        }

        public async Task<string?> GetDireccionTexto(string celular, decimal lat, decimal lng)
        {
            var url = $"api/OrdenPago/GetDireccionTexto?celular={Uri.EscapeDataString(celular)}&lat={lat}&lng={lng}";
            return await _httpClient.GetFromJsonAsync<string?>(url);
        }

        public async Task<decimal> GetSaldoCajaActual()
        {
            return await _httpClient.GetFromJsonAsync<decimal>("api/OrdenPago/GetSaldoCajaActual");
        }

        public async Task<OrdenPagoResultadoDTO> GenerarOrdenPago(GenerarOrdenPagoRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/OrdenPago/GenerarOrdenPago", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadFromJsonAsync<OrdenPagoResultadoDTO>() ?? new OrdenPagoResultadoDTO();
        }

        public async Task<List<OrdenPagoResumenDTO>> GetOrdenPagoPorEmpresa(string ruc, DateTime? hasta = null)
        {
            var url = $"api/OrdenPago/GetOrdenPagoPorEmpresa/{ruc}";
            if (hasta.HasValue)
                url += $"?hasta={hasta.Value:yyyy-MM-dd}";

            return await _httpClient.GetFromJsonAsync<List<OrdenPagoResumenDTO>>(url) ?? new List<OrdenPagoResumenDTO>();
        }

        public async Task ActualizarDatosPedido(ActualizarDatosPedidoRequestDTO request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/OrdenPago/ActualizarDatosPedido", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        public async Task<byte[]> ExportarFacturacionPdf(string ruc, string razonSocial, DateTime? hasta)
        {
            var url = $"api/OrdenPago/ExportarFacturacionPdf?ruc={Uri.EscapeDataString(ruc)}&razonSocial={Uri.EscapeDataString(razonSocial)}";
            if (hasta.HasValue)
                url += $"&hasta={hasta.Value:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<List<ReporteVoucherPagarDTO>> GetVouchersPendientesPorUnidad(string? unidad, DateTime desde, DateTime hasta)
        {
            var url = $"api/OrdenPago/GetVouchersPendientesPorUnidad?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(unidad))
                url += $"&unidad={Uri.EscapeDataString(unidad)}";

            return await _httpClient.GetFromJsonAsync<List<ReporteVoucherPagarDTO>>(url) ?? new List<ReporteVoucherPagarDTO>();
        }

        public async Task<byte[]> ExportarReporteVoucherPagarPdf(string? unidad, DateTime desde, DateTime hasta)
        {
            var url = $"api/OrdenPago/ExportarReporteVoucherPagarPdf?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
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

        public async Task<ResumenVoucherDTO> GetResumenVouchers(DateTime desde, DateTime hasta)
        {
            var url = $"api/OrdenPago/GetResumenVouchers?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            return await _httpClient.GetFromJsonAsync<ResumenVoucherDTO>(url) ?? new ResumenVoucherDTO();
        }

        public async Task<string?> GetMiRuc()
        {
            var response = await _httpClient.GetAsync("api/OrdenPago/GetMiRuc");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<string>();
        }

        public async Task<ResumenVoucherDTO> GetResumenVouchersEmpresa(DateTime desde, DateTime hasta)
        {
            var url = $"api/OrdenPago/GetResumenVouchersEmpresa?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}";
            return await _httpClient.GetFromJsonAsync<ResumenVoucherDTO>(url) ?? new ResumenVoucherDTO();
        }
    }
}
