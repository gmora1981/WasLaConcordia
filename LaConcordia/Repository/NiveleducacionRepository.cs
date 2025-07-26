using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class NiveleducacionRepository : INiveleducacion
    {
        private readonly HttpClient _httpClient;

        public NiveleducacionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NiveleducacionDTO>> GetNiveleducacionAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<NiveleducacionDTO>>("api/Niveleeducacion/GetNiveleducacionInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los niveles de educación.", ex);
            }
        }

        public async Task<NiveleducacionDTO> GetNiveleducacionById(int id)
        {
            return await _httpClient.GetFromJsonAsync<NiveleducacionDTO>($"api/Niveleeducacion/GetNiveleducacionById/{id}");
        }

        public async Task InsertNiveleducacion(NiveleducacionDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Niveleeducacion/InsertNiveleeducacion", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar nivel de educación: {errorContent}");
            }
        }


        public async Task UpdateNiveleducacion(NiveleducacionDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Niveleeducacion/UpdateNiveleeducacion", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar nivel de educación: {errorContent}");
            }
        }

        public async Task DeleteNiveleducacionById(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Niveleeducacion/DeleteNiveleducacionById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar nivel de educación: {errorContent}");
            }
        }

        // Paginado
        public async Task<PagedResult<NiveleducacionDTO>> GetNiveleducacionPaginados(int pagina,
            int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/Niveleeducacion/GetNiveleducacionPaginados?pagina={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(filtro))
            {
                url += $"&filtro={Uri.EscapeDataString(filtro)}";
            }
            if (!string.IsNullOrEmpty(estado))
            {
                url += $"&estado={Uri.EscapeDataString(estado)}";
            }
            return await _httpClient.GetFromJsonAsync<PagedResult<NiveleducacionDTO>>(url);
        }
    }
}
