using System.Net.Http.Json;
using LaConcordia.DTO;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class UnidadRepository : IUnidad
    {
        private readonly HttpClient _httpClient;

        public UnidadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UnidadDTO>> GetUnidadInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UnidadDTO>>("api/Unidad/GetUnidadInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las Unidades.", ex);
            }
        }

        public async Task<UnidadDTO> GetUnidadById(string id)
        {
            return await _httpClient.GetFromJsonAsync<UnidadDTO>($"api/Unidad/GetUnidadById/{id}");
        }

        public async Task InsertUnidad(UnidadDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Unidad/InsertUnidad", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Unidad: {errorContent}");
            }
        }

        public async Task UpdateUnidad(UnidadDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Unidad/UpdateUnidad", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Unidad: {errorContent}");
            }
        }

        public async Task DeleteUnidadById(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Unidad/DeleteUnidadById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Unidad: {errorContent}");
            }
        }

        public async Task<PagedResult<UnidadDTO>> GetUnidadPaginados(int pagina, int pageSize, string? Placa = null, string? Idpropietario = null, string? Unidad1 = null, string? Propietario = null, string? Estado = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "pagina", pagina.ToString() },
                { "pageSize", pageSize.ToString() }
            };
            if (!string.IsNullOrEmpty(Placa)) queryParams.Add("Placa", Placa);
            if (!string.IsNullOrEmpty(Idpropietario)) queryParams.Add("Idpropietario", Idpropietario);
            if (!string.IsNullOrEmpty(Unidad1)) queryParams.Add("Unidad1", Unidad1);
            if (!string.IsNullOrEmpty(Propietario)) queryParams.Add("Propietario", Propietario);
            if (!string.IsNullOrEmpty(Estado)) queryParams.Add("Estado", Estado);
            var response = await _httpClient.GetAsync($"api/Unidad/GetUnidadPaginados?" + await new FormUrlEncodedContent(queryParams).ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PagedResult<UnidadDTO>>();
        }

    }
}
