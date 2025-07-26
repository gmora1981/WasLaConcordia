using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class NacionalidadRepository : INacionalidad
    {
        private readonly HttpClient _httpClient;

        public NacionalidadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DTO.NacionalidadDTO>> GetNacionalidadesAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DTO.NacionalidadDTO>>("api/Nacionalidad/GetNacionalidadInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las nacionalidades.", ex);
            }
        }

        public async Task<DTO.NacionalidadDTO> GetNacionalidadById(int idnacionalidad)
        {
            return await _httpClient.GetFromJsonAsync<DTO.NacionalidadDTO>($"api/Nacionalidad/GetNacionalidadById/{idnacionalidad}");
        }

        public async Task InsertNacionalidad(DTO.NacionalidadDTO nacionalidad)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Nacionalidad/InsertNacionalidad", nacionalidad);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar nacionalidad: {errorContent}");
            }
        }

        public async Task UpdateNacionalidad(DTO.NacionalidadDTO nacionalidad)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Nacionalidad/UpdateNacionalidad", nacionalidad);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar nacionalidad: {errorContent}");
            }
        }

        public async Task DeleteNacionalidadById(int idnacionalidad)
        {
            var response = await _httpClient.DeleteAsync($"api/Nacionalidad/DeleteNacionalidadById/{idnacionalidad}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar nacionalidad: {errorContent}");
            }
        }

        public async Task<DTO.PagedResult<DTO.NacionalidadDTO>> GetNacionalidadesPaginados(int pagina, int pageSize, string? nacionalidad = null, string? estado = null)
        {
            var response = await _httpClient.GetAsync($"api/Nacionalidad/GetNacionalPaginados?pagina={pagina}&pageSize={pageSize}&nacionalidad={nacionalidad}&estado={estado}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DTO.PagedResult<DTO.NacionalidadDTO>>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener nacionalidades paginadas: {errorContent}");
            }
        }
    }
}
