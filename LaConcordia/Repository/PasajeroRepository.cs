using LaConcordia.DTO;
using LaConcordia.Interface;
using Modelo.laconcordia.Modelo.Database;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class PasajeroRepository : IPasajero
    {
        private readonly HttpClient _httpClient;

        public PasajeroRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pasajero>> GetPasajeroInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Pasajero>>("api/Pasajero/GetPasajeroInfoAll") ?? new List<Pasajero>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Pasajeros.", ex);
            }
        }

        public async Task<PasajeroDTO?> GetPasajeroByCelular(string celular)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PasajeroDTO>($"api/Pasajero/GetPasajeroByCelular/{celular}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener el Pasajero.", ex);
            }
        }

        public async Task InsertPasajero(Pasajero newItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pasajero/InsertPasajero", newItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar Pasajero: {errorContent}");
            }
        }

        public async Task UpdatePasajero(Pasajero updItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Pasajero/UpdatePasajero", updItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar Pasajero: {errorContent}");
            }
        }

        public async Task DeletePasajeroByCelular(string celular)
        {
            var response = await _httpClient.DeleteAsync($"api/Pasajero/DeletePasajeroByCelular/{celular}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Pasajero: {errorContent}");
            }
        }

        public async Task<PagedResult<Pasajero>> GetPasajerosPaginados(int pagina, int pageSize, string? nombres = null, string? celular = null)
        {
            var url = $"api/Pasajero/GetPasajeroPaginados?pagina={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(nombres))
                url += $"&nombres={Uri.EscapeDataString(nombres)}";
            if (!string.IsNullOrEmpty(celular))
                url += $"&celular={Uri.EscapeDataString(celular)}";

            try
            {
                return await _httpClient.GetFromJsonAsync<PagedResult<Pasajero>>(url) ?? new PagedResult<Pasajero>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los Pasajeros paginados.", ex);
            }
        }
    }
}
