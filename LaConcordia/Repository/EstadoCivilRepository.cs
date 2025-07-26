using System.Net.Http.Json;
using LaConcordia.DTO;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class EstadoCivilRepository : IEstadoCivil
    {
        private readonly HttpClient _httpClient;

        public EstadoCivilRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstadoCivilDTO>> GetEstadoCivilAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<EstadoCivilDTO>>("api/EstadoCivil/GetEstadoCivilAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los estados civiles.", ex);
            }
        }

        public async Task<EstadoCivilDTO> GetEstadoCivilById(int id)
        {
            return await _httpClient.GetFromJsonAsync<EstadoCivilDTO>($"api/EstadoCivil/GetEstadoCivilById/{id}");
        }

        public async Task InsertEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EstadoCivil/InsertEstadoCivil", estadoCivil);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Estado Civil: {errorContent}");
            }
        }

        public async Task UpdateEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            var response = await _httpClient.PutAsJsonAsync("api/EstadoCivil/UpdateEstadoCivil", estadoCivil);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Estado Civil: {errorContent}");
            }
        }

        public async Task DeleteEstadoCivilById(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/EstadoCivil/DeleteEstadoCivilById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Estado Civil: {errorContent}");
            }
        }

        // Paginado
        public async Task<PagedResult<EstadoCivilDTO>> GetEstadoCivilPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/EstadoCivil/GetEstadoCivilPaginados?pagina={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(filtro))
            {
                url += $"&filtro={Uri.EscapeDataString(filtro)}";
            }
            if (!string.IsNullOrEmpty(estado))
            {
                url += $"&estado={Uri.EscapeDataString(estado)}";
            }
            return await _httpClient.GetFromJsonAsync<PagedResult<EstadoCivilDTO>>(url);


        }
    }
}
