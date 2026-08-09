using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class BalanceRepository : IBalance
    {
        private readonly HttpClient _httpClient;

        public BalanceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BalanceResultadoDTO> GetBalance(DateTime fechaDesde, DateTime fechaHasta)
        {
            var url = $"api/Balance/GetBalance?fechaDesde={fechaDesde:yyyy-MM-dd}&fechaHasta={fechaHasta:yyyy-MM-dd}";
            try
            {
                return await _httpClient.GetFromJsonAsync<BalanceResultadoDTO>(url) ?? new BalanceResultadoDTO();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al generar el balance.", ex);
            }
        }

        public async Task<byte[]> ExportarPdf(DateTime fechaDesde, DateTime fechaHasta)
        {
            var url = $"api/Balance/ExportarPdf?fechaDesde={fechaDesde:yyyy-MM-dd}&fechaHasta={fechaHasta:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar el balance: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
