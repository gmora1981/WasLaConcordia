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
    }
}
