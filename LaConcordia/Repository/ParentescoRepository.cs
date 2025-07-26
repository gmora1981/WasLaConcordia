using System.Net.Http.Json;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class ParentescoRepository : IParentesco
    {
        private readonly HttpClient _httpClient;

        public ParentescoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DTO.ParentescoDTO>> ParentescoInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DTO.ParentescoDTO>>("api/Parentesco/ParentescoInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Parentescos.", ex);
            }
        }

        public async Task<DTO.ParentescoDTO> GetParentescoById(int id)
        {
            return await _httpClient.GetFromJsonAsync<DTO.ParentescoDTO>($"api/Parentesco/GetParentescoById/{id}");
        }

        public async Task InsertParentesco(DTO.ParentescoDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Parentesco/InsertParentesco", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Parentesco: {errorContent}");
            }
        }

        public async Task UpdateParentesco(DTO.ParentescoDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Parentesco/UpdateParentesco", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Parentesco: {errorContent}");
            }
        }

        public async Task DeleteParentescoById(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Parentesco/DeleteParentescoById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Parentesco: {errorContent}");
            }
        }

        // Paginado
        public async Task<DTO.PagedResult<DTO.ParentescoDTO>> GetParentescosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            var query = $"pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(filtro))
                query += $"&parentesco1={Uri.EscapeDataString(filtro)}";

            if (!string.IsNullOrEmpty(estado))
                query += $"&estado={Uri.EscapeDataString(estado)}";

            var url = $"api/Parentesco/GetParentescoPaginados?{query}";

            return await _httpClient.GetFromJsonAsync<DTO.PagedResult<DTO.ParentescoDTO>>(url);
        }


    }
}
