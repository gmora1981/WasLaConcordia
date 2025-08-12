using LaConcordia.DTO;
using System.Net.Http.Json;
using LaConcordia.Interface;

namespace LaConcordia.Repository
{
    public class SegurovidumRepository : ISegurovidum
    {
        private readonly HttpClient _httpClient;

        public SegurovidumRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SegurovidumDTO>> GetSegurovidumInfoAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<SegurovidumDTO>>(
                    "api/Segurovidum/GetSegurovidumInfoAll");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Hubo un error al obtener los seguros vidum.", ex);
            }
        }

        public async Task<List<SegurovidumDTO>> GetSegurovidumByCedula(string CiAfiliado)
        {
            return await _httpClient.GetFromJsonAsync<List<SegurovidumDTO>>(
                $"api/Segurovidum/GetSegurovidumByCedula/{CiAfiliado}");
        }

        public async Task InsertSegurovidum(SegurovidumDTO nueva)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Segurovidum/InsertSegurovidum", nueva);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al insertar seguro vidum: {errorContent}");
            }
        }

        public async Task UpdateSegurovidum(SegurovidumDTO actualizada)
        {
            var response = await _httpClient.PutAsJsonAsync(
                "api/Segurovidum/UpdateSegurovidum", actualizada);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar seguro vidum: {errorContent}");
            }
        }

        public async Task DeleteSegurovidumByCedula(int CiBeneficiario)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/Segurovidum/DeleteSegurovidumByCedula/{CiBeneficiario}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar seguro vidum: {errorContent}");
            }
        }

        public async Task<PagedResult<SegurovidumDTO>> GetSegurovidumPaginados(int pagina, int pageSize, string CiBeneficiario = null, string CiAfiliado = null)
        {
            var url = $"api/Segurovidum/GetSegurovidumPaginados?pagina={pagina}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(CiBeneficiario))
                url += $"&CiBeneficiario={CiBeneficiario}";
            if (!string.IsNullOrEmpty(CiAfiliado))
                url += $"&CiAfiliado={CiAfiliado}";
            return await _httpClient.GetFromJsonAsync<PagedResult<SegurovidumDTO>>(url);
        }

        public async Task<PagedResult<SegurovidumDTO>> GetSegurovidumPaginadosByCedulaAfiliado(int pagina, int pageSize, string CiAfiliado)
        {
            var url = $"api/Segurovidum/GetSegurovidumPaginadosByCedulaAfiliado?pagina={pagina}&pageSize={pageSize}&CiAfiliado={CiAfiliado}";
            return await _httpClient.GetFromJsonAsync<PagedResult<SegurovidumDTO>>(url);
        }
    }
}
