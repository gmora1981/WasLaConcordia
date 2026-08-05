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
    }
}
