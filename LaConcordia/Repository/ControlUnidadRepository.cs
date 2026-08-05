using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class ControlUnidadRepository : IControlUnidad
    {
        private readonly HttpClient _httpClient;

        public ControlUnidadRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UnidadServicioDTO>> GetFichaPersonalPorServicio(string estadoServicio)
        {
            return await _httpClient.GetFromJsonAsync<List<UnidadServicioDTO>>($"api/ControlUnidad/GetFichaPersonalPorServicio/{estadoServicio}") ?? new List<UnidadServicioDTO>();
        }

        public async Task MoverUnidad(MoverUnidadRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ControlUnidad/MoverUnidad", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }
    }
}
