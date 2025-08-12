using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class FichapersonalRepository : IFichapersonal
    {
        private readonly HttpClient _httpClient;

        public FichapersonalRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FichapersonalDTO>> GetFichaPersonalInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FichapersonalDTO>>("api/Fichapersona/GetFichaPersonalInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener las fichas personas.", ex);
            }
        }

        public async Task<FichapersonalDTO> GetFichaPersonalById(string cedula)
        {
            return await _httpClient.GetFromJsonAsync<FichapersonalDTO>($"api/Fichapersona/GetFichaPersonalById/{cedula}");
        }

        public async Task InsertFichaPersonal(FichapersonalDTO New)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Fichapersona/InsertFichaPersonal", New);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar InsertEmpresa: {errorContent}");
            }
        }

        public async Task UpdateFichaPersonal(FichapersonalDTO UpdItem)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Fichapersona/UpdateFichaPersonal", UpdItem);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar ficha personal: {errorContent}");
            }
        }

        public async Task DeleteFichaPersonalById(string cedula)
        {
            var response = await _httpClient.DeleteAsync($"api/Fichapersona/DeleteFichaPersonalById/ {cedula}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar Departamento: {errorContent}");
            }
        }
        //paginado

        public async Task<PagedResult<FichapersonalDTO>> GetFichaPersonalPaginados(int pagina,
        int pageSize, string? filtro = null, string? estado = null)
        {
            var url = $"api/Fichapersona/GetFichaPersonalPaginados?pagina={pagina}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(filtro))
                url += $"&filtro={Uri.EscapeDataString(filtro)}"; // CORREGIDO aquí

            if (!string.IsNullOrEmpty(estado))
                url += $"&estado={Uri.EscapeDataString(estado)}";

            try
            {
                var result = await _httpClient.GetFromJsonAsync<PagedResult<FichapersonalDTO>>(url);
                return result ?? new PagedResult<FichapersonalDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener ficha personal paginadas desde el servidor.", ex);
            }
        }
        //exportar
        public async Task<byte[]> ExportarFichaCompleta(ExportFichaDTO exportData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Fichapersona/ExportarFichaCompleta", exportData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al exportar ficha completa: {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }


    }
}