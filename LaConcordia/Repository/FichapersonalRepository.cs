using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Headers;
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

        // 🚀 Repository - Subir imagen del chofer
        public async Task SubirImagenChoferAsync(Stream contenido, string nombreArchivo, string cedulaChofer)
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(contenido);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            content.Add(streamContent, "archivo", nombreArchivo); // ⚠️ coincide con [FromForm] IFormFile archivo
            content.Add(new StringContent(cedulaChofer), "cedula");

            try
            {
                var response = await _httpClient.PostAsync("api/Fichapersona/SubirImagenChofer", content);

                if (!response.IsSuccessStatusCode)
                {
                    var mensaje = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al subir imagen: {mensaje}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al subir la imagen: {ex.Message}", ex);
            }
        }

        // 🚀 Repository - Buscar imagen del chofer por cédula
        public async Task<string?> BuscarImagenChoferAsync(string cedulaChofer)
        {
            var response = await _httpClient.GetAsync($"api/Fichapersona/BuscarImagenChofer/{cedulaChofer}");

            if (!response.IsSuccessStatusCode)
                return null;

            var url = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(url) ? null : url;
        }

        // 🚀 Repository - Eliminar imagen del chofer
        public async Task EliminarImagenChoferAsync(string cedulaChofer)
        {
            var response = await _httpClient.DeleteAsync($"api/Fichapersona/EliminarImagenChofer/{cedulaChofer}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar imagen: {error}");
            }
        }


    }
}