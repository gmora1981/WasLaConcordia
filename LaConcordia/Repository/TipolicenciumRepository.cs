using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class TipolicenciumRepository : ITipolicencium
    {
        private readonly HttpClient _httpClient;
        public TipolicenciumRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<TipolicenciumDTO>> GetTipolicenciaInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TipolicenciumDTO>>("api/Tipolicencium/GetTipolicenciaInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las Tipos de Licencia.", ex);
            }
        }
        public async Task<TipolicenciumDTO> GetTipolicenciaById(int idTipoLicencia)
        {
            return await _httpClient.GetFromJsonAsync<TipolicenciumDTO>($"api/Tipolicencium/GetTipolicenciaById/{idTipoLicencia}");
        }
        public async Task InsertTipolicencia(TipolicenciumDTO nuevaDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tipolicencium/InsertTipolicencia", nuevaDto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Tipolicencia: {errorContent}");
            }
        }
        public async Task UpdateTipolicencia(TipolicenciumDTO actualizada)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Tipolicencium/UpdateTipolicencia", actualizada);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Tipolicencia: {errorContent}");
            }
        }
        public async Task DeleteEmpresaByRuc(int idTipoLicencia)
        {
            var response = await _httpClient.DeleteAsync($"api/Tipolicencium/DeleteEmpresaByRuc/{idTipoLicencia}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Tipolicencia: {errorContent}");
            }
        }

        // Paginado
        public async Task<PagedResult<TipolicenciumDTO>> GetTipoLicenciumPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/Tipolicencium/GetTipoLicenciumPaginados?page={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(filtro))
            {
                url += $"&filtro={Uri.EscapeDataString(filtro)}";
            }
            if (!string.IsNullOrEmpty(estado))
            {
                url += $"&estado={Uri.EscapeDataString(estado)}";
            }
            return await _httpClient.GetFromJsonAsync<PagedResult<TipolicenciumDTO>>(url);
        }
    }
}
