using LaConcordia.DTO;
using LaConcordia.Interface;
using System.Net.Http.Json;

namespace LaConcordia.Repository
{
    public class SolicitudCarreraRepository : ISolicitudCarrera
    {
        private readonly HttpClient _httpClient;

        public SolicitudCarreraRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CrearSolicitud(CrearSolicitudCarreraRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/SolicitudCarrera/CrearSolicitud", request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        public async Task<List<SolicitudCarreraDTO>> GetMisSolicitudes()
        {
            return await _httpClient.GetFromJsonAsync<List<SolicitudCarreraDTO>>("api/SolicitudCarrera/GetMisSolicitudes") ?? new List<SolicitudCarreraDTO>();
        }

        public async Task<List<SolicitudCarreraDTO>> GetSolicitudesPendientes()
        {
            return await _httpClient.GetFromJsonAsync<List<SolicitudCarreraDTO>>("api/SolicitudCarrera/GetSolicitudesPendientes") ?? new List<SolicitudCarreraDTO>();
        }

        public async Task MarcarConvertida(int idsolicitud)
        {
            var response = await _httpClient.PostAsync($"api/SolicitudCarrera/MarcarConvertida/{idsolicitud}", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }
    }
}
