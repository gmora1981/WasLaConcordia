using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class FlujoCajaRepository : IFlujoCaja
    {
        private readonly HttpClient _httpClient;

        public FlujoCajaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FlujoCajaDTO>> GetFlujoCajaInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FlujoCajaDTO>>("api/FlujoCaja/GetFlujoCajaInfoAll") ?? new List<FlujoCajaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener el flujo de caja.", ex);
            }
        }

        public async Task<FlujoCajaDTO?> GetUltimoRegistro()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FlujoCajaDTO?>("api/FlujoCaja/GetUltimoRegistro");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener el ultimo registro de caja.", ex);
            }
        }

        public async Task InsertFlujoCaja(FlujoCajaDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/FlujoCaja/InsertFlujoCaja", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al registrar el movimiento de caja: {errorContent}");
            }
        }

        // Paginado
        public async Task<PagedResult<FlujoCajaDTO>> GetFlujoCajaPaginados(
            int pagina,
            int pageSize,
            DateTime? fechaDesde = null,
            DateTime? fechaHasta = null,
            string? concepto = null)
        {
            var url = $"api/FlujoCaja/GetFlujoCajaPaginados?pagina={pagina}&pageSize={pageSize}";
            if (fechaDesde.HasValue)
                url += $"&fechaDesde={fechaDesde.Value:o}";
            if (fechaHasta.HasValue)
                url += $"&fechaHasta={fechaHasta.Value:o}";
            if (!string.IsNullOrEmpty(concepto))
                url += $"&concepto={Uri.EscapeDataString(concepto)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<FlujoCajaDTO>>(url) ?? new PagedResult<FlujoCajaDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener el flujo de caja paginado.", ex);
            }
        }
    }
}
