using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class CargoRepository : ICargo
    {

        private readonly HttpClient _httpClient;
        public CargoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CargoDTO>> GetCargoInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CargoDTO>>("api/Cargo/GetCargoInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Cargos.", ex);
            }
        }
        public async Task<CargoDTO> GetCargoById(int id)
        {
            return await _httpClient.GetFromJsonAsync<CargoDTO>($"api/Cargo/GetCargoById/{id}");
        }
        public async Task InsertCargo(CargoDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cargo/InsertCargo", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Cargo: {errorContent}");
            }
        }
        public async Task UpdateCargo(CargoDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Cargo/UpdateCargo", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Cargo: {errorContent}");
            }
        }
        public async Task DeleteCargoById(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Cargo/DeleteCargoById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Cargo: {errorContent}");
            }
        }
        // Paginado
        public async Task<PagedResult<CargoDTO>> GetCargosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/Cargo/GetCargoPaginados?page={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(filtro))
                url += $"&filtro={Uri.EscapeDataString(filtro)}";
            if (!string.IsNullOrEmpty(estado))
                url += $"&estado={Uri.EscapeDataString(estado)}";
            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<CargoDTO>>(url);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Cargos paginados.", ex);
            }

        }
    }
}
