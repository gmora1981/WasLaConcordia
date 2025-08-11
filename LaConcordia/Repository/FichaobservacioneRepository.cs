using LaConcordia.DTO;
using System.Net.Http.Json;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class FichaobservacioneRepository : IFichaobservacione
    {
        private readonly HttpClient _httpClient;

        public FichaobservacioneRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FichaobservacioneDTO>> GetFichaObservacioneInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FichaobservacioneDTO>>(
                    "api/Fichaobservacione/GetFichaObservacioneInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las fichas de observaciones.", ex);
            }
        }

        public async Task<List<FichaobservacioneDTO>> GetFichaObservacioneByCedula(string cedula)
        {
            return await _httpClient.GetFromJsonAsync<List<FichaobservacioneDTO>>(
                $"api/Fichaobservacione/GetFichaObservacioneByCedula/{cedula}");
        }

        public async Task InsertFichaObservacione(FichaobservacioneDTO nueva)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Fichaobservacione/InsertFichaObservacione", nueva);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar ficha de observación: {errorContent}");
            }
        }

        public async Task UpdateFichaObservacione(FichaobservacioneDTO actualizada)
        {
            var response = await _httpClient.PutAsJsonAsync(
                "api/Fichaobservacione/UpdateFichaObservacione", actualizada);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar ficha de observación: {errorContent}");
            }
        }

        public async Task DeleteFichaObservacioneByCedula(int cedula)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/Fichaobservacione/DeleteFichaObservacioneByCedula/{cedula}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar ficha de observación: {errorContent}");
            }
        }

        public async Task<PagedResult<FichaobservacioneDTO>> GetFichaObservacionePaginados(
            int pagina, int pageSize, string unidad = null, string cedula = null)
        {
            var query = $"pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(unidad))
                query += $"&unidad={Uri.EscapeDataString(unidad)}";

            if (!string.IsNullOrEmpty(cedula))
                query += $"&cedula={Uri.EscapeDataString(cedula)}";

            var url = $"api/Fichaobservacione/GetFichaObservacionePaginados?{query}";

            return await _httpClient.GetFromJsonAsync<PagedResult<FichaobservacioneDTO>>(url);
        }

        public async Task<PagedResult<FichaobservacioneDTO>> GetFichaObservacionePaginadosByCedula(
            int pagina, int pageSize, string fkCedula)
        {
            var query = $"pagina={pagina}&pageSize={pageSize}&fkCedula={Uri.EscapeDataString(fkCedula)}";
            var url = $"api/Fichaobservacione/GetFichaObservacionePaginadosByCedula?{query}";

            return await _httpClient.GetFromJsonAsync<PagedResult<FichaobservacioneDTO>>(url);
        }
    }
}
