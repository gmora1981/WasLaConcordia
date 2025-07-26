using System.Net.Http.Json;
using LaConcordia.DTO;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class DuenopuestoRepository : IDuenopuesto
    {
        private readonly HttpClient _httpClient;
        public DuenopuestoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DTO.DuenopuestoDTO>> DuenopuestoInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DTO.DuenopuestoDTO>>("api/Duenopuesto/DuenopuestoInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los dueños de puestos.", ex);
            }
        }
        public async Task<DTO.DuenopuestoDTO> GetDuenopuestoByCedula(string cedula)
        {
            return await _httpClient.GetFromJsonAsync<DTO.DuenopuestoDTO>($"api/Duenopuesto/GetDuenopuestoById/{cedula}");
        }
        public async Task InsertDuenopuesto(DTO.DuenopuestoDTO duenopuesto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Duenopuesto/InsertDuenopuesto", duenopuesto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Duenopuesto: {errorContent}");
            }
        }
        public async Task UpdateDuenopuesto(DTO.DuenopuestoDTO duenopuesto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Duenopuesto/UpdateDuenopuesto", duenopuesto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Duenopuesto: {errorContent}");
            }
        }
        public async Task DeleteDuenopuestoByCedula(string cedula)
        {
            var response = await _httpClient.DeleteAsync($"api/Duenopuesto/DeleteDuenopuestoById/{cedula}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Duenopuesto: {errorContent}");
            }
        }

        //paginado
        public async Task<PagedResult<DuenopuestoDTO>> GetDuenopuestosPaginados(
    int pagina,
    int pageSize,
    string? cedula = null,
    string? nombre = null,
    string? apellidos = null,
    string? estado = null)
        {
            var queryParams = new List<string>
    {
        $"pagina={pagina}",
        $"pageSize={pageSize}"
    };

            if (!string.IsNullOrWhiteSpace(cedula))
                queryParams.Add($"cedula={Uri.EscapeDataString(cedula)}");

            if (!string.IsNullOrWhiteSpace(nombre))
                queryParams.Add($"nombre={Uri.EscapeDataString(nombre)}");

            if (!string.IsNullOrWhiteSpace(apellidos))
                queryParams.Add($"apellidos={Uri.EscapeDataString(apellidos)}");

            if (!string.IsNullOrWhiteSpace(estado))
                queryParams.Add($"estado={Uri.EscapeDataString(estado)}");

            var url = $"api/Duenopuesto/GetDuenopuestosPaginados?{string.Join("&", queryParams)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<DuenopuestoDTO>>(url);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los dueños de puestos paginados.", ex);
            }
        }
    }
}
